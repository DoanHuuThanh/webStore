using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace doanhuuthanh_web.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; } //giá gốc
        public int Stock { get; set; } // phiếu
        public int ViewCount { get; set; } //số lượng xem
        public DateTime DateCreated { get; set; } // ngày tạo 
        public string? SeoAlias { get; set; }

        public List<ProductInCategory> ProductInCategories { get; set; }

        public List<OrderDetails> OrderDetail { get; set; }

        public List<Cart> Carts { get; set; }

        public List<ProductTranslation> ProductTranslations { get; set; }

        public List<ProductImage>  ProductImages { get; set; }
    }
}
