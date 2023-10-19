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
    internal class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImage");
            builder.HasKey("Id");
            builder.Property(x=>x.Id).UseIdentityColumn();

            builder.Property(x=>x.ImagePath).HasMaxLength(200).IsRequired(true);
            builder.Property(x => x.ImagePath).HasMaxLength(200).IsRequired(false);


            builder.HasOne(x=>x.Product).WithMany(x=>x.ProductImages).HasForeignKey(x=>x.ProductId);
        }
    }
}
