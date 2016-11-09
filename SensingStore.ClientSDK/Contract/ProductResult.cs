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
        public List<ProductImage> ProductImages { get; set; }
    }

    public class ProductImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string ImageSizeType { get; set; }

        public int ProductId { get; set; }
    }
}
