using AppPod.DataAccess.Models;
using Sensing.SDK.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppPod.DataAccess
{
    public interface ILocalSensingDataAccess
    {
        //ProductInfo FindByProductId(int productId);
        ProductSdkModel FindByScanId(string scanId);
        List<ShowProductInfo> FindSimilar(int itemId);
        ProductSdkModel FindByProductId(int productId);
        List<ShowProductInfo> QueryShowProducts(bool onlySpu);
        List<ProductSdkModel> GetProductsByCategroyName(string categroyName);
        List<ProductSdkModel> GetProductsByCategroyNames(string[] categroyNames);

        List<AdsSdkModel> Ads { get; set; }
        List<StaffSdkModel> Staffs { get; set; }
        List<ProductSdkModel> Products { get; set; }
        List<CouponViewModel> Coupons { get; set; }
        List<ProductCategorySDKModel> PCategories { get; set; }

        List<MatchInfoViewModel> Matches { get; set; }
        List<LikeInfoViewModel> Likes { get; set; }

        //List<ProductInfo> GetProductsByCategroyName(IEnumerable<string> categroyNames);
        List<ProductSdkModel> SearchProductsByName(string searchTerm);
        //ProductInfo FindProductByAttribute(string productAttribute);
        List<ProductSdkModel> SearchProducts(float minPrice, float maxPrice, List<string> colors, List<int> categoryIds, List<string> tags);
         List<ProductCategorySDKModel> GetCategroyInfos(bool isSpecial = true);
        //List<ProductInfo> GetProductInfos();
        List<CouponViewModel> GetCoupons();
        List<AdsSdkModel> GetAdsInfos();
        //List<StaffInfo> GetStaffInfos();
        void Like(ProductSdkModel productInfo);
        //void Click(ProductInfo productInfo);
        //void LoadProducts();
        //string GetLocalImage(ProductInfo p);
        //string GetLocalCategroyImage(CategroyInfo ca);
        //string GetLocalCategoryIcon(CategroyInfo ca);
        //string GetProgress();
        bool IsCompleted();
        Task Intialize();
        Task<List<AdsSdkModel>> ReadAdsAsync();
        Task<List<MatchInfoViewModel>> ReadProductMatchesAsync();
        Task<List<LikeInfoViewModel>> ReadProductLikesAsync();
        Task<List<CouponViewModel>> ReadCouponsAsync();
        Task<List<ProductSdkModel>> ReadProductsAsync();
        Task<List<ProductCategorySDKModel>> ReadCategorysAsync();
        Task<List<StaffSdkModel>> ReadStaffsAsync();
    }
}
