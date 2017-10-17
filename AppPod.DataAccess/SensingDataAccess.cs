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

        #region Bussiness Logical Data
        public async Task<string> GetOnlineStoreStaffId(int staffId, string storeType)
        {
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

        public List<ShowProductInfo> QueryShowProducts(bool onlySpu = false)
        {
            if (Products == null || Products.Count == 0) return null;
            if(onlySpu)
            {
                var infos = Products.Select(pModel => new ShowProductInfo
                {
                    Id = pModel.Id,
                    ImageUrl = pModel.PicUrl,
                    Name = pModel.Title,
                    Price = pModel.Price,
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
                                ImageUrl = prod.PicUrl,
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
                                ImageUrl = prod.PicUrl,
                                Quantity = prod.Num,
                                Name = prod.Title,
                                Price = prod.Price,
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
                                    ImageUrl = pImg.ImageUrl,
                                    Quantity = firstSku.Quantity,
                                    Name = firstSku.Title,
                                    Price = firstSku.Price,
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
            return PCategories.Where(p => p.IsSpecial).ToList();
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

        public List<ShowProductInfo> FindSimilar(int itemId)
        {
            //todo 
            return QueryShowProducts(true);
        }
        public bool IsCompleted()
        {
            return true;
        }
        #endregion

        #region AppPod Base
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

        public List<AdsSdkModel> Ads { get; set; }
        public List<StaffSdkModel> Staffs { get; set; }
        public List<ProductSdkModel> Products { get; set; }
        public List<CouponViewModel> Coupons { get; set; }
        public List<ProductCategorySDKModel> PCategories { get; set; }

        public List<MatchInfoViewModel> Matches { get; set; }
        public List<LikeInfoViewModel> Likes { get; set; }


        #region Read Data from Local Json.
        public async Task<List<AdsSdkModel>> ReadAdsAsync()
        {
            var path = $"{AppPodDataDirectory}/Ads/Ads.json";
            if (!File.Exists(path)) return null;
            string json = await ReadTextAsync(path);
            return JsonConvert.DeserializeObject<List<AdsSdkModel>>(json);
        }

        public async Task<List<MatchInfoViewModel>> ReadProductMatchesAsync()
        {
            var path = $"{AppPodDataDirectory}/Products/Matches.json";
            if (!File.Exists(path)) return null;
            string json = await ReadTextAsync(path);
            return JsonConvert.DeserializeObject<List<MatchInfoViewModel>>(json);
        }

        public async Task<List<LikeInfoViewModel>> ReadProductLikesAsync()
        {
            var path = $"{AppPodDataDirectory}/Products/Likes.json";
            if (!File.Exists(path)) return null;
            string json = await ReadTextAsync(path);
            return JsonConvert.DeserializeObject<List<LikeInfoViewModel>>(json);
        }

        public async Task<List<CouponViewModel>> ReadCouponsAsync()
        {
            var path = $"{AppPodDataDirectory}/Products/Coupons.json";
            if (!File.Exists(path)) return null;
            string json = await ReadTextAsync(path);
            return JsonConvert.DeserializeObject<List<CouponViewModel>>(json);
        }

        public async Task<List<ProductSdkModel>> ReadProductsAsync()
        {
            var path = $"{AppPodDataDirectory}/Products/Products.json";
            if (!File.Exists(path)) return null;
            string json = await ReadTextAsync(path);
            return JsonConvert.DeserializeObject<List<ProductSdkModel>>(json);
        }

        public async Task<List<ProductCategorySDKModel>> ReadCategorysAsync()
        {
            var path = $"{AppPodDataDirectory}/Products/Categories.json";
            if (!File.Exists(path)) return null;
            string json = await ReadTextAsync(path);
            return JsonConvert.DeserializeObject<List<ProductCategorySDKModel>>(json);

        }

        public async Task<List<StaffSdkModel>> ReadStaffsAsync()
        {
            var path = $"{AppPodDataDirectory}/Staffs/Staffs.json";
            if (!File.Exists(path)) return null;
            string json = await ReadTextAsync(path);
            return JsonConvert.DeserializeObject<List<StaffSdkModel>>(json);
        }

        public static async Task<string> ReadTextAsync(string filePath)
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

        public async Task Intialize()
        {
            Ads = await ReadAdsAsync();
            Staffs = await ReadStaffsAsync();
            Products = await ReadProductsAsync();
            PCategories = await ReadCategorysAsync();
            Coupons = await ReadCouponsAsync();
            Matches = await ReadProductMatchesAsync();
            Likes = await ReadProductLikesAsync();

        }

    }
}
