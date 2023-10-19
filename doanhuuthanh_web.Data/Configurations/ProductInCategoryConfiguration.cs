using doanhuuthanh_web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doanhuuthanh_web.Data.Configurations
{
    public class ProductInCategoryConfiguration : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
           builder.HasKey(t => new {t.ProductId,t.CategoryId});
            builder.ToTable("ProductInCategory");
            //Đoạn mã này định nghĩa mối quan hệ giữa bảng ProductInCategory và bảng Product. Mối quan hệ này sử dụng một khóa ngoại
            //(foreign key) đến cột ProductId trong bảng ProductInCategory, liên kết với khóa chính (primary key) của bảng Product.
            //Mối quan hệ này biểu thị rằng một bản ghi trong bảng ProductInCategory liên kết với một bản ghi trong bảng Product.
            builder.HasOne(t => t.Product).WithMany(pc => pc.ProductInCategories)
                                          .HasForeignKey(pc => pc.ProductId);

            builder.HasOne(t => t.Category).WithMany(pc => pc.ProductInCategories)
                                         .HasForeignKey(pc => pc.CategoryId);
        }
        //một mối quan hệ n-nhiều giữa bảng Product và bảng Category thông qua bảng trung gian ProductInCategory.
        //Điều này cho phép bạn thêm một sản phẩm vào một hoặc nhiều danh mục và một danh mục có thể chứa nhiều sản phẩm, tạo thành mối quan hệ n-nhiều giữa chúng.
    }
}
