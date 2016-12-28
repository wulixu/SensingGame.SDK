using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hooters.ClientSDK.Contract
{
    public class DeviceResult
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public DeviceInfo Data { get; set; }
    }

    public class GroupResult
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
    }

    public class OrderResult
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }

    public class ReportsResult
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<ReportInfo> Data { get; set; }
    }

    public class ReportInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Type { get; set; }
    }

    public class ReportDataResult
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<ReportDataInfo> Data { get; set; }
    }

    public class ReportDataInfo
    {
        public string DataSource { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string Value { get; set; }
    }



    public class ReportDigitalDataResult
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public ReportDigitalDataInfo Data { get; set; }
    }

    public class  ReportDigitalDataInfo
    {
        public decimal CurrentValue { get; set; }
        
        public decimal DenomValue { get; set; }
    }

}
