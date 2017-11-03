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
        List<ShowProductInfo> FindSimilar(ShowProductInfo itemId, bool useSameSpu = true, bool useSameCategories = false);
        ProductSdkModel FindByProductId(int productId);
        List<ShowProductInfo> QueryShowProducts(bool onlySpu);
        List<ProductSdkModel> GetProductsByCategroyName(string categroyName);
        List<ProductSdkModel> GetProductsByCategroyNames(string[] categroyNames);
        List<ShowProductInfo> GetShowProductsByCategroyName(string categroyName);
        List<ShowProductInfo> GetShowProductByCategoryNames(int[] categroyIds);
        string GetOnlineStoreStaffId(int staffId);
        string GetOnlineStoreStaffIdByRFID(string rfidCode);
        string GetStoreType();

        List<AdsSdkModel> Ads { get; set; }
        List<StaffSdkModel> Staffs { get; set; }
        List<ProductSdkModel> Products { get; set; }
        List<CouponViewModel> Coupons { get; set; }
        List<ProductCategorySDKModel> PCategories { get; set; }


        List<MatchInfoViewModel> Matches { get; set; }
        List<LikeInfoViewModel> Likes { get; set; }

        //List<ProductInfo> GetProductsByCategroyName(IEnumerable<string> categroyNames);
        List<ShowProductInfo> SearchShowProductsByName(string searchTerm);
        //ProductInfo FindProductByAttribute(string productAttribute);
        List<ShowProductInfo> SearchProducts(List<Range<float>> priceRanges, List<string> colors, List<int> categories, List<int> tags, bool onlySpu = false);
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
