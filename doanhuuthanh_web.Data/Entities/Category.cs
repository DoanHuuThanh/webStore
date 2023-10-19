using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using doanhuuthanh_web.Data.Enums;


namespace doanhuuthanh_web.Data.Entities
{
    public class Category
    {
        /// <summary>
        /// Kha chinh
        /// </summary>
        public int Id { get; set; }
        public int SortOder { get; set; }
        public bool IsShowOnHome { get; set; }
        public int? parentId { get; set; }
        public Statuscs Status { get; set; }

        public List<ProductInCategory> ProductInCategories { get; set; }
        public List<CategoryTranslation> CategoryTranslations { get; set; }
    }
}
