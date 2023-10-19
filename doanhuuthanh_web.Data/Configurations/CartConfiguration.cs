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
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Cart");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn(); //hi bạn thêm một bản ghi mới vào bảng Cart mà không cung cấp giá trị cho cột Id, cơ sở dữ liệu sẽ tự động tạo một giá trị duy nhất cho cột Id dựa trên tính năng Identity.

            builder.HasOne(x => x.Product).WithMany(x => x.Carts).HasForeignKey(x => x.ProductId);
            builder.Property(x => x.Price).HasPrecision(10, 2);
            builder.HasOne( x=> x.AppUsers).WithMany(x=>x.Carts).HasForeignKey(x => x.UserId);

        }
    }
}
