using Sensing.SDK.Contract;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPod.DataAccess
{
    public static class Extensions
    {
        public static string GetLocalCategoryImage(this ProductCategorySDKModel category)
        {
            if (category == null || string.IsNullOrEmpty(category.ImageUrl)) return null;
            var localPath = ExtractSchema(category.ImageUrl);
            return $"{SensingDataAccess.AppPodDataDirectory}\\Products\\{localPath}";
        }

        public static string ExtractSchema(string fileName)
        {
            if (fileName == null) return null;
            string fileNamePath = fileName;
            if (fileName.StartsWith("http", true, CultureInfo.CurrentCulture))
            {
                var uri = new Uri(fileName).LocalPath;
                fileNamePath = uri;
            }
            return ChangeFileName(fileNamePath);
        }

        public static string ChangeFileName(string fileNamePath)
        {
            if (IsSS2File(fileNamePath))
            {
                var fileName = fileNamePath.Substring(0, fileNamePath.Length - 1 - 4);
                return fileName + ".jpg";
            }
            return fileNamePath;
        }

        public static bool IsSS2File(string f)
        {
            return f != null &&
                f.EndsWith(".SS2", StringComparison.Ordinal);
        }
    }
}
