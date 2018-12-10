using AppPod.DataAccess.Models;
using Sensing.SDK;
using Sensing.SDK.Contract;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AppPod.DataAccess
{
    public interface IBehaviorDataUploader
    {
        void AddBehavoirData(string thingId, string thingName, string category, string action, string softwareName = "", string pageName = "");
        void AddClick(AdsSdkModel ads, string softwareName, string pageName);
        void AddLike(ShowProductInfo productInfo, string softwareName, string pageName);
        void AddClick(ShowProductInfo productInfo, string softwareName, string pageName);
        List<ClickInfo> ReadClickData();
        List<ClickInfo> ReadLikeClickData();
        void LogDeviceStatus(int secondInterval);

    }

    public class BehaviroStorage : IBehaviorDataUploader
    {
        private SensingWebClient sesingWebClient;

        private string DBPath = "Behavior.db";
        private SQLite.SQLiteConnection m_db;
        private object lockobject = new object();
        private string mMac;
        private DateTime mLastUploadTime;

        public BehaviroStorage(SensingWebClient webClient, string mac)
        {
            sesingWebClient = webClient;
            mMac = mac;
            m_db = new SQLite.SQLiteConnection(DBPath);
            m_db.CreateTable<SqlLiteBehaviorRecord>();
            m_db.CreateTable<SqliteDeviceStatus>();
        }

        public void AddClick(AdsSdkModel ads, string softwareName, string pageName)
        {
            if (ads == null) return;
            Task.Factory.StartNew(() =>
            {
                AddBehavoirData(ads.Id.ToString(), "ads", "click", softwareName, pageName);
            });
        }

        public void AddLike(ShowProductInfo productInfo, string softwareName, string pageName)
        {
            if (productInfo == null) return;
            Task.Factory.StartNew(() => 
            {
                AddBehavoirData(productInfo.Id.ToString(), productInfo.Name,productInfo.Type.ToString(), "like", softwareName, pageName);
            });
        }


        public void AddClick(ShowProductInfo productInfo, string softwareName, string pageName)
        {
            if (productInfo == null) return;
            Task.Factory.StartNew(() =>
            {
                AddBehavoirData(productInfo.Id.ToString(), productInfo.Name, productInfo.Type.ToString(), "click", softwareName, pageName);
            });
        }

        public void AddMatch(ShowProductInfo productInfo, string softwareName, string pageName)
        {
            if (productInfo == null) return;
            Task.Factory.StartNew(() =>
            {
                AddBehavoirData(productInfo.Id.ToString(), productInfo.Name, productInfo.Type.ToString(), "match", softwareName, pageName);
            });
        }

        public void AddBehavoirData(string thingId, string thingName, string category,string action,string softwareName = "", string pageName ="")
        {
            //todo:william.
            Task.Factory.StartNew(() =>
            {
                if (string.IsNullOrEmpty(action)) return;
                var updateRecord = m_db.Table<SqlLiteBehaviorRecord>().FirstOrDefault(r => r.ThingId == thingId && r.Category == category && r.Action == action && r.IsSynced == false);
                if(updateRecord == null)
                {
                    SqlLiteBehaviorRecord record = new SqlLiteBehaviorRecord();
                    record.Action = action;
                    record.CollectionTime = DateTime.Now;
                    record.CollectEndTime = DateTime.Now;
                    record.Increment = 1;
                    record.ThingId = thingId;
                    record.Name = thingName;
                    record.SoftwareName = softwareName;
                    record.PageName = pageName;
                    record.Category = category;
                    record.IsSynced = false;
                    m_db.Insert(record);
                }
                else
                {
                    updateRecord.Increment++;
                    m_db.Update(updateRecord);
                }
                UploadData();
            });
           
        }

        public List<ClickInfo> ReadClickData()
        {
            var query = m_db.Query<ClickInfo>("select ThingId,count(*) ClickCount from SqlLiteBehaviorRecord where Action='click' group by ThingId");
            return query;
        }

        public List<ClickInfo> ReadLikeClickData()
        {
            var query = m_db.Query<ClickInfo>("select ThingId,count(*) ClickCount from SqlLiteBehaviorRecord where Action='like' group by ThingId");
            return query;
        }

        public List<ClickInfo> ReadMatchClickData()
        {
            var query = m_db.Query<ClickInfo>("select ThingId,count(*) ClickCount from SqlLiteBehaviorRecord where Action='match' group by ThingId");
            return query;
        }

        private void UploadData()
        {
            if (DateTime.Now.Subtract(mLastUploadTime).TotalMinutes < 30)
                return;
            var records = m_db.Table<SqlLiteBehaviorRecord>().Where(r => r.IsSynced == false).Take(50).ToList();
            if (records.Count() > 0)
            {
                bool success = sesingWebClient.PostBehaviorRecordsAsync(records).GetAwaiter().GetResult();
                if (success)
                {
                    foreach (var r in records)
                    {
                        r.IsSynced = true;
                    }
                    m_db.UpdateAll(records);
                    mLastUploadTime = DateTime.Now;
                }
                int deletedCount = m_db.Execute("delete from SqlLiteBehaviorRecord where IsSynced =? and  CollectionTime < ?", true, DateTime.Today.AddDays(-15));
            }
        }


        public void LogDeviceStatus(int secondInterval)
        {
            var now = DateTime.Now;
            var record = m_db.Table<SqliteDeviceStatus>().Where(r => r.IsSynced == false).OrderByDescending(d => d.EndTime).Take(1).FirstOrDefault();
            //var cpu = GetCpuUsage();
            //MEMORY_INFO memInfo = new MEMORY_INFO();
            //GlobalMemoryStatus(ref memInfo);
            //var memory = memInfo.dwMemoryLoad;
            if (record != null && record.EndTime.Subtract(now).Days == 0)
            {
                if(now.Subtract(record.EndTime).TotalSeconds > secondInterval)
                {
                    var newRecord = new SqliteDeviceStatus { StartTime = now, EndTime = now, IsSynced = false, Cpu = 0, Memory = 0 };
                    m_db.Insert(newRecord);
                }
                else
                {
                    record.EndTime = now;
                    //if (cpu > record.Cpu) record.Cpu = cpu;
                    //if (memory > record.Memory) record.Memory = memory;
                    m_db.Update(record);
                }
            }
            else
            {
                var newRecord = new SqliteDeviceStatus { StartTime = now, EndTime = now, IsSynced = false, Cpu = 0, Memory = 0 };
                m_db.Insert(newRecord);
            }
            //todo: update to cloud.
            SyncToCloud();
        }

        public void SyncToCloud()
        {
            var now = DateTime.Now;
            var records = m_db.Table<SqliteDeviceStatus>().Where(r => r.IsSynced == false).OrderByDescending(d => d.EndTime).Skip(1).OrderBy(d => d.EndTime).Take(10).ToList();
            if (records.Count() > 0)
            {
                bool success = sesingWebClient.PostDeviceStatusRecordAsync(records).GetAwaiter().GetResult();
                if (success)
                {
                    foreach (var r in records)
                    {
                        r.IsSynced = true;
                    }
                    m_db.UpdateAll(records);
                }
            }
        }
        public int GetCpuUsage()
        {
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total", "MyComputer");
            cpuCounter.NextValue();
            System.Threading.Thread.Sleep(1000);
            return (int)cpuCounter.NextValue();
        }

        [DllImport("kernel32")]
        public static extern void GlobalMemoryStatus(ref MEMORY_INFO meminfo);
    }
    //定义内存的信息结构    
    [StructLayout(LayoutKind.Sequential)]
    public struct MEMORY_INFO
    {
        public uint dwLength;
        public uint dwMemoryLoad;
        public uint dwTotalPhys;
        public uint dwAvailPhys;
        public uint dwTotalPageFile;
        public uint dwAvailPageFile;
        public uint dwTotalVirtual;
        public uint dwAvailVirtual;
    }
    public  class SqlLiteBehaviorRecord : BehaviorRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public bool IsSynced { get; set; }
    }

    public class SqliteDeviceStatus : DeviceStatusInput
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public bool IsSynced { get; set; }
    }
}
