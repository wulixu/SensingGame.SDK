using AppPod.DataAccess.Models;
using Newtonsoft.Json;
using Sensing.SDK.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPod.DataAccess
{
    public class SensingDataAccess : ILocalSensingDataAccess
    {
        public static string AppPodDataDirectory { get; set; }
        public static DeviceSetting DeviceSetting { get; set; }
        public SensingDataAccess(bool isAutoFindAppPodDataDirecotry = true)
        {
            if(isAutoFindAppPodDataDirecotry)
            {
                AppPodDataDirectory = FindAppPodDataFolder();
                DeviceSetting = FindDeviceSetting();
            }
        }

        public SensingDataAccess(string appPodDataDirectory)
        {
            AppPodDataDirectory = appPodDataDirectory;
        }

        #region Bussiness Logical Data
        public string GetOnlineStoreStaffId(int staffId)
        {
            var storeType = GetStoreType();
            var staff = Staffs.Find(s => s.Id == staffId);
            if (staff == null) return null;
            var onlineStaff = staff.OnlineStoreProfiles.AsQueryable().FirstOrDefault(s => s.OnlineStoreType == storeType);
            if (onlineStaff == null) return staff.Code;
            return onlineStaff.Code;
       }

        public ProductSdkModel FindByProductId(int id)
        {
            return Products?.Find(p => p.Id == id);
        }


        public ProductSdkModel FindByScanId(string skc)
        {
            throw new Exception();
        }


        public static string GetLocalImagePath(string path,string category)
        {
            if (string.IsNullOrEmpty(path)) return null;
            var localPath = Extensions.ExtractSchema(path);
            return $"{SensingDataAccess.AppPodDataDirectory}\\{category}\\res\\{localPath}";
        }


        public List<ShowProductInfo> QueryShowProducts(bool onlySpu = false)
        {
            if (Products == null || Products.Count == 0) return null;
            string storeType = GetStoreType();
            if(onlySpu)
            {
                var infos = Products.Select(pModel => new ShowProductInfo
                {
                    Id = pModel.Id,
                    ImageUrl = GetLocalImagePath(pModel.PicUrl,"Product"),
                    Name = pModel.Title,
                    Price = pModel.Price,
                    QrcodeUrl = pModel.OnlineStoreInfos.FirstOrDefault(s => s.OnlineStoreType == storeType)?.Qrcode,
                    Type = ProductType.Product
                }).ToList();
                return infos;
            }
            else
            {
                var showProducts = new List<ShowProductInfo>();
                foreach(var prod in Products)
                {
                    if(prod.Skus == null)
                    {
                        if (prod.HasRealSkus == false)
                        {
                            showProducts.Add(new ShowProductInfo
                            {
                                Id = prod.Id,
                                ImageUrl = GetLocalImagePath(prod.PicUrl,"Products"),
                                Name = prod.Title,
                                Price = prod.Price,
                                Quantity = prod.Num,
                                Type = ProductType.Product
                            });
                        }
                        continue;
                    }
                    if(prod.Skus != null && prod.Skus.Count() == 0)
                    {
                        if (!prod.HasRealSkus)
                        {
                            showProducts.Add(new ShowProductInfo
                            {
                                Id = prod.Id,
                                ImageUrl = GetLocalImagePath(prod.PicUrl, "Products"),
                                Quantity = prod.Num,
                                Name = prod.Title,
                                Price = prod.Price,
                                QrcodeUrl = prod.OnlineStoreInfos.FirstOrDefault(s => s.OnlineStoreType == storeType)?.Qrcode,
                                Type = ProductType.Product
                            });
                        }
                        continue;
                    }
                    if(prod.PropImgs != null && prod.PropImgs.Count() > 0)
                    {
                        foreach(var pImg in prod.PropImgs)
                        { 
                            var keyProps = pImg.PropertyName;
                            var firstSku = prod.Skus.AsQueryable().FirstOrDefault(s => s.PropsName.Contains(keyProps));
                            if(firstSku != null)
                            {
                                showProducts.Add(new ShowProductInfo
                                {
                                    Id = firstSku.Id,
                                    ImageUrl = GetLocalImagePath(pImg.ImageUrl, "Products"),
                                    Quantity = firstSku.Quantity,
                                    Name = firstSku.Title,
                                    Price = firstSku.Price,
                                    QrcodeUrl = prod.OnlineStoreInfos.FirstOrDefault(s => s.OnlineStoreType == storeType)?.Qrcode,
                                    Type = ProductType.Sku
                                });
                            }
                        }
                    }
                }
                return showProducts;
            }
        }


        public List<ProductSdkModel> GetProductsByCategroyName(string categroyName)
        {
            var category = FindCategoryByName(categroyName);
            if (category != null)
            {
                return Products.Where(p => p.CategoryIds != null && p.CategoryIds.Contains(category.Id)).ToList();
            }
            return null;
        }

        public List<ProductSdkModel> GetProductsByCategroyNames(string[] categroyNames)
        {
            var ids = FindCategoryIdsByNames(categroyNames);
            if (Products == null || ids == null) return null;
            var prods = new List<ProductSdkModel>();
            foreach(var p in Products)
            {
                foreach(var id in ids)
                {
                    if (p.CategoryIds.Contains(id))
                    {
                        prods.Add(p);
                        break;
                    }
                }
            }
            return prods;
        }

        public ProductCategorySDKModel FindCategoryByName(string categoryName)
        {
            return PCategories.Find(p => p.Name.Contains(categoryName));
        }

        public List<int> FindCategoryIdsByNames(string[] categoryNames)
        {
            return PCategories.Where(p => categoryNames.Any(c => c ==p.Name)).Select(s => s.Id).ToList();
        }
        public List<ProductCategorySDKModel> GetCategroyInfos(bool isSpecial = true)
        {
            return PCategories?.Where(p => p.IsSpecial).ToList();
        }

        public List<CouponViewModel> GetCoupons()
        {
            return Coupons.ToList();
        }

        public List<ProductSdkModel> SearchProducts(float minPrice, float maxPrice, List<string> colors, List<int> categoryIds, List<string> tags)
        {
            throw new NotImplementedException();
        }

        public List<ProductSdkModel> SearchProductsByName(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public void Like(ProductSdkModel productInfo)
        {
            throw new NotImplementedException();
        }
        public List<AdsSdkModel> GetAdsInfos()
        {
            return Ads;
        }
        public bool IsCompleted()
        {
            return true;
        }
        #endregion

        #region AppPod Base
        public static string FindAppPodRootFolder()
        {
            var exeRoot = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo root = new DirectoryInfo(exeRoot);
            while (root != null)
            {
                if (File.Exists(Path.Combine(root.FullName, "AppPod.exe")))
                {
                    return root.FullName;
                }
                root = root.Parent;
            }
            return null;
        }

        public static string FindAppPodDataFolder()
        {
            var rootFolder = FindAppPodRootFolder();
            if (rootFolder == null)
                return null;
            return Path.Combine(rootFolder, "AppPodData");
             
        }

        private static DeviceSetting FindDeviceSetting()
        {
            var rootFolder = FindAppPodRootFolder();
            if (rootFolder == null)
                return null;
            var sqlconnection = new SQLite.SQLiteConnection(rootFolder + "/_data/AppPodData.db");
            return sqlconnection.Table<DeviceSetting>().FirstOrDefault();
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

        public List<AdsSdkModel> Ads { get; set; }
        public List<StaffSdkModel> Staffs { get; set; }
        public List<ProductSdkModel> Products { get; set; }
        public List<CouponViewModel> Coupons { get; set; }
        public List<ProductCategorySDKModel> PCategories { get; set; }

        public List<MatchInfoViewModel> Matches { get; set; }
        public List<LikeInfoViewModel> Likes { get; set; }


        #region Read Data from Local Json.
        public List<AdsSdkModel> ReadAds()
        {
            var path = $"{AppPodDataDirectory}/Ads/Ads.json";
            if (!File.Exists(path)) return null;
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<AdsSdkModel>>(json);
        }

        public List<MatchInfoViewModel> ReadProductMatches()
        {
            var path = $"{AppPodDataDirectory}/Products/Matches.json";
            if (!File.Exists(path)) return null;
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<MatchInfoViewModel>>(json);
        }

        public List<LikeInfoViewModel> ReadProductLikes()
        {
            var path = $"{AppPodDataDirectory}/Products/Likes.json";
            if (!File.Exists(path)) return null;
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<LikeInfoViewModel>>(json);
        }

        public List<CouponViewModel> ReadCoupons()
        {
            var path = $"{AppPodDataDirectory}/Products/Coupons.json";
            if (!File.Exists(path)) return null;
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<CouponViewModel>>(json);
        }

        public List<ProductSdkModel> ReadProducts()
        {
            var path = $"{AppPodDataDirectory}/Products/Products.json";
            if (!File.Exists(path)) return null;
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<ProductSdkModel>>(json);
        }

        public List<ProductCategorySDKModel> ReadCategorys()
        {
            var path = $"{AppPodDataDirectory}/Products/Categories.json";
            if (!File.Exists(path)) return null;
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<ProductCategorySDKModel>>(json);

        }

        public List<StaffSdkModel> ReadStaffs()
        {
            var path = $"{AppPodDataDirectory}/Staffs/Staffs.json";
            if (!File.Exists(path)) return null;
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<StaffSdkModel>>(json);
        }

        public static async Task<string> ReadText(string filePath)
        {
            using (FileStream sourceStream = new FileStream(filePath,
                FileMode.Open, FileAccess.Read, FileShare.Read,
                bufferSize: 4096, useAsync: true))
            {
                StringBuilder sb = new StringBuilder();

                byte[] buffer = new byte[0x1000];
                int numRead;
                while ((numRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    string text = Encoding.Unicode.GetString(buffer, 0, numRead);
                    sb.Append(text);
                }

                return sb.ToString();
            }
        }
        #endregion

        public bool Intialize()
        {
            Ads = ReadAds();
            Staffs = ReadStaffs();
            Products = ReadProducts();
            PCategories = ReadCategorys();
            Coupons = ReadCoupons();
            Matches = ReadProductMatches();
            Likes = ReadProductLikes();
            return true;
        }

        public List<ShowProductInfo>  DistinctShowProducts(ProductSdkModel prod,int exceptSkuId = -1)
        {
            if (prod == null) return null;
            if (prod.PropImgs != null && prod.PropImgs.Count() > 0)
            {
                var showProducts = new List<ShowProductInfo>();
                foreach (var pImg in prod.PropImgs)
                {
                    var keyProps = pImg.PropertyName;
                    var firstSku = prod.Skus.AsQueryable().FirstOrDefault(s => s.PropsName.Contains(keyProps));
                    if (firstSku != null)
                    {
                        if (firstSku.Id != exceptSkuId)
                        {
                            showProducts.Add(new ShowProductInfo
                            {
                                Id = firstSku.Id,
                                ImageUrl = GetLocalImagePath(pImg.ImageUrl, "Products"),
                                Quantity = firstSku.Quantity,
                                Name = firstSku.Title,
                                Price = firstSku.Price,
                                Type = ProductType.Sku
                            });
                        }
                    }
                }
                return showProducts;
            }
            return null;
        }

        public List<ShowProductInfo> FindSimilar(ShowProductInfo productInfo, bool useSameSpu = true, bool useSameCategories = false)
        {
            //todo:Qu.
            if (productInfo == null) return null;
            List<ShowProductInfo> similarSkus = new List<ShowProductInfo>();
            if (useSameSpu == false && useSameCategories == false)
            {
                similarSkus.Add(productInfo);
                return similarSkus;
            }
            if(productInfo.Type == ProductType.Product)
            {

            }
            if(productInfo.Type == ProductType.Sku)
            {
                if (useSameSpu)
                {
                    var spu = Products?.FirstOrDefault(p => p.Skus.Any(s => s.Id == productInfo.Id));
                    similarSkus = DistinctShowProducts(spu, productInfo.Id);
                    similarSkus.Insert(0, productInfo);
                }
                return similarSkus;
            }
            return null;
        }

        public string GetStoreType()
        {
            if (DeviceSetting == null)
                return "Taobao";
            return DeviceSetting.OnlineTrafficTarget.ToString();
        }
    }
}
