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
    public class ProductTransactionConfiguration : IEntityTypeConfiguration<ProductTranslation>
    {
        public void Configure(EntityTypeBuilder<ProductTranslation> builder)
        {
            builder.ToTable("ProductTranslation");
            builder.HasKey(t => t.Id);
            builder.Property(t =>t.Id).UseIdentityColumn();
            builder.Property(t => t.Name).IsRequired().HasMaxLength(200);
            builder.Property(t =>t.SeoAlias).IsRequired().HasMaxLength(200);
            builder.Property(t => t.Details).HasMaxLength(500);

            builder.Property(x => x.LanguageId).IsUnicode(false).IsRequired().HasMaxLength(5);
            builder.HasOne(t => t.Product).WithMany(t => t.ProductTranslations).HasForeignKey(t => t.ProductId);
            builder.HasOne(T => T.Language).WithMany(t => t.ProductTranslations).HasForeignKey(t => t.LanguageId);

        }
    }
}
