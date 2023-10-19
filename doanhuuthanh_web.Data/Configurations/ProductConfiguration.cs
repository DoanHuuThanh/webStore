using doanhuuthanh_web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doanhuuthanh_web.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.ToTable("Products");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();


            builder.Property(x => x.Price).IsRequired().HasPrecision(10, 2);

            builder.Property(x => x.OriginalPrice).IsRequired().HasPrecision(10, 2);

            builder.Property(x => x.Stock).IsRequired().HasDefaultValue(0);

            builder.Property(x => x.ViewCount).IsRequired().HasDefaultValue(0);
            //HasDefaultValue(0): Đoạn mã này đặt giá trị mặc định cho thuộc tính Stock là 0.
            //Điều này có nghĩa là nếu không cung cấp giá trị cho thuộc tính Stock khi tạo mới một bản ghi, giá trị mặc định sẽ là 0.



        }
    }
}
