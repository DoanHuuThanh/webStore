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
    public class LanguageConfiguration : IEntityTypeConfiguration<Languages>
    {
        public void Configure(EntityTypeBuilder<Languages> builder)
        {
            builder.ToTable("Languages");
            builder.HasKey(x => x.id);
            builder.Property(x => x.id).IsRequired().IsUnicode(false).HasMaxLength(5); //tiết kiệm không gian bộ nhớ , tìm kiếm nhanh hơn, tương thích vs hệ thống cũ
            //IsUnicode(false): Đoạn mã này xác định rằng thuộc tính id không được lưu dưới dạng Unicode. Điều này có ý nghĩa là chỉ sử dụng bộ mã ASCII cho giá trị của id,
            //chứ không sử dụng bộ mã Unicode. Khi đặt giá trị là false, cột trong cơ sở dữ liệu sẽ sử dụng bộ mã ANSI, thích hợp cho các ký tự ASCII.

            builder.Property(x => x.name).IsRequired().HasMaxLength(200);
        }
    }
}
