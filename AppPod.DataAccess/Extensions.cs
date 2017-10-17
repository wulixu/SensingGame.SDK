using AppPod.DataAccess.Models;
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

        public static string GetLocalCategoryIcon(this ProductCategorySDKModel category)
        {
            if (category == null || string.IsNullOrEmpty(category.IconUrl)) return null;
            var localPath = ExtractSchema(category.IconUrl);
            return $"{SensingDataAccess.AppPodDataDirectory}\\Products\\{localPath}";
        }

        public static string GetLocalImage(this ShowProductInfo productInfo)
        {
            if (productInfo == null || string.IsNullOrEmpty(productInfo.ImageUrl)) return null;
            var localPath = ExtractSchema(productInfo.ImageUrl);
            return $"{SensingDataAccess.AppPodDataDirectory}\\Products\\{localPath}";
        }

        public static ShowProductInfo ToShowProductInfo(this ProductSdkModel productInfo)
        {
            return new ShowProductInfo
            {
                Id = productInfo.Id,
                ImageUrl = productInfo.PicUrl,
                Name = productInfo.Title,
                Price = productInfo.Price,
                Quantity = productInfo.Num,
                Type = ProductType.Product
            };
        }

        public static List<ShowProductInfo> ToShowProductInfo(this List<ProductSdkModel> productInfos)
        {
            return productInfos.Select(p => new ShowProductInfo
            {
                Id = p.Id,
                ImageUrl = p.PicUrl,
                Name = p.Title,
                Price = p.Price,
                Quantity = p.Num,
                Type = ProductType.Product
            }).ToList();
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
