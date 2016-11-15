using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensingStore.ClientSDK.Contract
{
    public class ProductResult
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<GroupInfo> Data { get; set; }
    }

    public class GroupInfo
    {
        public string SellerId { get; set; }
        public string StoreId { get; set; }
        public List<ProductInfo> Products { get; set; }
    }

    public class ProductInfo
    {
        public int Id { get; set; }
        public string ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Pic_url { get; set; }

        public string QRCodeUrl { get; set; }
        //public int ProductCategoryId { get; set; }
        public List<ProductImageInfo> ProductImages { get; set; }
        public List<ProductCategoryInfo> productCategorys { get; set; }
    }

    public class ProductImageInfo
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string ImageSizeType { get; set; }

        public int ProductId { get; set; }
    }

    public class ProductCategoryInfo
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CategoryNum { get; set; }
        public string CategoryName { get; set; }
    }
}
