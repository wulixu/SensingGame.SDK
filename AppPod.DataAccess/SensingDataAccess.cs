using Sensing.SDK.Contract;
using System;
using System.IO;

namespace AppPod.DataAccess
{
    public class SensingDataAccess
    {
        public string AppPodDataDirectory { get; set; }
        public SensingDataAccess(bool isAutoFindAppPodDataDirecotry = true)
        {
            if(isAutoFindAppPodDataDirecotry)
            {
                AppPodDataDirectory = FindAppPodDataFolder();
            }
        }

        public SensingDataAccess(string appPodDataDirectory)
        {
            AppPodDataDirectory = appPodDataDirectory;
        }

        #region Base
        public static string FindAppPodDataFolder()
        {
            var exeRoot = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo root = new DirectoryInfo(exeRoot);
            while (root != null)
            {
                if (File.Exists(Path.Combine(root.FullName, "AppPod.exe")))
                {
                    return root.FullName + "/";
                }
                root = root.Parent;
            }
            return null;
        }

        private static string FindResourceFolder()
        {
            var exeRoot = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo root = new DirectoryInfo(exeRoot);
            while (root != null)
            {
                if (File.Exists(Path.Combine(root.FullName, "AppPod.exe")))
                {
                    return root.FullName + "/AppPodData/";
                }
                root = root.Parent;
            }
            return null;
        }
        #endregion

        //public List<ProductSDKModel> LoadProducts()
        //{

        //}
    }
}
