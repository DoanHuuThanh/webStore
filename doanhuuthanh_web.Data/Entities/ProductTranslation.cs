using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doanhuuthanh_web.Data.Entities
{
    public class ProductTranslation
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } // miểu tả
        public string Details { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTitle { get; set;}
        public string SeoAlias { get; set; }
        public string LanguageId { get; set; }
        public Product Product { get; set; }
        public Languages Language { get; set; }

    }
}
