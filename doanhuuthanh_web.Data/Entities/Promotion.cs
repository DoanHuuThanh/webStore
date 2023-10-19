using doanhuuthanh_web.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doanhuuthanh_web.Data.Entities
{
    public class Promotion
    {
        public int Id { get; set; }
        public DateTime FromDate { get; set; }  
        public DateTime ToDate { get; set; }
        public bool ApplyForAll { get; set; }
        public int? DiscountPercent { get; set; } // phần trăm giảm giá
        public decimal? DiscountAmount { get; set; } // chiết khấu
        public string ProductIds { get; set; }  
        public string ProductCategoryIds { get; set; }
        public Statuscs Statuscs { get; set; }
        public string Name { get; set; }

    }
}
