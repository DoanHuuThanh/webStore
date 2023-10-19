using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace doanhuuthanh_web.ViewModel.Catalog.Products
{
    public class ProductCreateRequest
    {
        public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; } //giá gốc
        public int Stock { get; set; } // phiếu
        public string Name { get; set; }
        public string Description { get; set; } // miểu tả
        public string Details { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTitle { get; set; }
        public string SeoAlias { get; set; }
        public string LanguageId { get; set; }
        public IFormFile ThumbNewImage { get; set; }

    }
}
