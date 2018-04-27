﻿using AppPod.DataAccess.Models;
using Sensing.SDK.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppPod.DataAccess
{
    public interface ILocalSensingDataAccess
    {
        ShowProductInfo GetShowProductInfoById(ProductType type, int id);
        string GetQrcode(SkuSdkModel pModel, string staffId);
        string GetQrcode(ProductSdkModel pModel, string staffId);
        string GetQrcode(ShowProductInfo showProductInfo, string staffId);
        List<PropertyInfo> FindReminderAvailablePropertiesInSkus(ShowProductInfo productSdkModel, params string[] nameValues);
        PropertyInfo GetKeyPropertyInfoInSkus(ProductSdkModel product);
        SkuSdkModel GetSkuSdkModelByShowProduct(ShowProductInfo showProductInfo);
        ProductSdkModel FindByShowProduct(ShowProductInfo showProductInfo);
        List<PropertyInfo> GetPropertyInfosInSkus(ProductSdkModel product);
        PropertyInfo GetKeyPropertyInfoInSkus(ShowProductInfo info);
        List<PropertyInfo> GetPropertyInfosInSkus(ShowProductInfo product);
        (string Key, string Value) GetPropertyName(string properties);
        ProductSdkModel FindByScanId(string scanId);
        ProductSdkModel FindByRfidCode(string rfid);
        List<ShowProductInfo> FindSimilar(ShowProductInfo itemId, bool useSameSpu = true, bool useSameCategories = false);
        ProductSdkModel FindByProductId(int productId);
        ProductSdkModel FindBySkuId(int skuId);
        SkuSdkModel FindSkuById(int skuId);
        List<ShowProductInfo> QueryShowProducts(bool onlySpu);
        List<ProductSdkModel> GetProductsByCategroyName(string categroyName);
        List<ProductSdkModel> GetProductsByCategroyNames(string[] categroyNames);
        List<ShowProductInfo> GetShowProductsByCategroyName(string categroyName);
        List<ShowProductInfo> GetShowProductByCategoryNames(int[] categroyIds);
        string GetOnlineStoreStaffId(int staffId);
        string GetOnlineStoreStaffIdByRFID(string rfidCode);
        string GetStoreType();
        string GetLocalImagePath(string path, string category);

        List<TagSdkModel> GetTagInfos();

        List<AdsSdkModel> Ads { get; set; }
        List<StaffSdkModel> Staffs { get; set; }
        List<ProductSdkModel> Products { get; set; }
        List<CouponViewModel> Coupons { get; set; }
        List<ProductCategorySDKModel> PCategories { get; set; }


        List<MatchInfoViewModel> Matches { get; set; }
        List<LikeInfoViewModel> Likes { get; set; }
        List<PropertyViewModel> Properties { get; set; }

        List<ShowProductInfo> QueryShowProductsByProperties(IDictionary<string,string> keyValues);
        List<ShowProductInfo> SearchShowProductsByName(string searchTerm);
        //ProductInfo FindProductByAttribute(string productAttribute);
        List<ShowProductInfo> SearchProducts(List<Range<float>> priceRanges, List<string> colors, List<int> categories, List<int> tags, List<string> keywords, bool onlySpu = false);
        List<ProductCategorySDKModel> GetCategroyInfos(bool isSpecial = true);
        /// <summary>
        /// 获取一级分类
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProductCategorySDKModel> GetRootCategories();

        /// <summary>
        /// 根据父分类id获取子分类
        /// </summary>
        /// <param name="parentCategoryId"></param>
        /// <returns></returns>
        IEnumerable<ProductCategorySDKModel> GetChildrenCategory(int parentCategoryId);
        List<ProductCategorySDKModel> GetUsefulCategroyInfos();
        //List<ProductInfo> GetProductInfos();
        List<CouponViewModel> GetCoupons();
        List<AdsSdkModel> GetAdsInfos();
        //List<StaffInfo> GetStaffInfos();
        void Like(ProductSdkModel productInfo);
        Task<List<ProductCommentModel>> GetProductComments(int productId);

        //void Click(ProductInfo productInfo);
        //void LoadProducts();
        //string GetLocalImage(ProductInfo p);
        //string GetLocalCategroyImage(CategroyInfo ca);
        //string GetLocalCategoryIcon(CategroyInfo ca);
        //string GetProgress();
        bool IsCompleted();
        bool Intialize();
        List<AdsSdkModel> ReadAds();
        List<MatchInfoViewModel> ReadProductMatches();
        List<LikeInfoViewModel> ReadProductLikes();
        List<CouponViewModel> ReadCoupons();
        List<ProductSdkModel> ReadProducts();
        List<ProductCategorySDKModel> ReadCategorys();
        List<TagSdkModel> ReadTags();
        List<StaffSdkModel> ReadStaffs();
    }
}
