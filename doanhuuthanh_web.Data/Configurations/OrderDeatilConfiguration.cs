using doanhuuthanh_web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace doanhuuthanh_web.Data.Configurations
{
    public class OrderDeatilConfiguration : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.ToTable("OrderDetail");
            builder.HasKey(x => new { x.OrderId, x.ProductId });
            builder.Property(x => x.Price).HasPrecision(10, 2);
            builder.HasOne(x => x.Order).WithMany(x => x.OrderDetail).HasForeignKey(x=>x.OrderId);
            builder.HasOne(x => x.OrderedProduct).WithMany(x => x.OrderDetail).HasForeignKey(x => x.ProductId);
        }
    }
}
