using Sensing.SDK.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPod.DataAccess.Models
{
    public enum ProductType
    {
        Product,
        Sku
    }
    public class ShowProductInfo
    {
        public int Id { get; set; }
        public ProductType Type { get; set; }
        public string Name { get; set; }

        public long Quantity { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public string TagIconUrl { get; set; }
        public string QrcodeUrl { get; set; }
        public ProductSdkModel Product { get; set; }
    }
}
