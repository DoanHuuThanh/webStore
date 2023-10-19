using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doanhuuthanh_web.ViewModel.Catalog.ProductImage
{
    public class ProductImagheViewModel
    {
        public int Id { get; set; }
        public int ProductId { get ; set; }
        public string ImagePath { get; set; }
        public bool IsDefault { get; set; }
        public long FileSize { get; set; }
        public DateTime DateCreated { get; set; }
        public int SortOrder { get; set; }  
        public string Caption { get; set; } 
    }
}
