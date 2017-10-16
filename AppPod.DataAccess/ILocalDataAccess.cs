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
        //ProductInfo FindByScanId(string scanId);
        //List<ProductInfo> FindSimilar(int itemId);
        List<ProductSdkModel> GetProductsByCategroyName(string categroyName);

        List<AdsSdkModel> Ads { get; set; }
        List<StaffSdkModel> Staffs { get; set; }
        List<ProductSdkModel> Products { get; set; }
        List<CouponViewModel> Coupons { get; set; }
        List<ProductCategorySDKModel> PCategories { get; set; }

        List<MatchInfoViewModel> Matches { get; set; }
        List<LikeInfoViewModel> Likes { get; set; }

        //List<ProductInfo> GetProductsByCategroyName(IEnumerable<string> categroyNames);
        //List<ProductInfo> SearchProductsBySku(string sku);
        //ProductInfo FindProductByAttribute(string productAttribute);
        //List<ProductInfo> SearchProducts(Range<float> price, List<string> colors, List<int> categoryIds, List<string> tags);
        //List<CategroyInfo> GetCategroyInfos(bool isSpecial = true);
        //List<ProductInfo> GetProductInfos();
        //List<CouponInfo> GetCoupons();
        //List<AdsInfo> GetAdsInfos();
        //List<StaffInfo> GetStaffInfos();
        //void Like(ProductInfo productInfo);
        //void Click(ProductInfo productInfo);
        //void LoadProducts();
        //string GetLocalImage(ProductInfo p);
        //string GetLocalCategroyImage(CategroyInfo ca);
        //string GetLocalCategoryIcon(CategroyInfo ca);
        //string GetProgress();
        //bool IsCompleted();
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
