using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doanhuuthanh_web.ViewModel.Catalog.Products.Dtos
{
    public class ProductViewModels
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; } //giá gốc
        public int Stock { get; set; } // phiếu
        public int ViewCount { get; set; } //số lượng xem
        public DateTime DateCreated { get; set; } // ngày tạo 
        public string Name { get; set; }
        public string Description { get; set; } // miểu tả
        public string Details { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTitle { get; set; }
        public string SeoAlias { get; set; }
        public string LanguageId { get; set; }
    }
}
