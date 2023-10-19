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
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Amount).HasPrecision(10, 2);
            builder.Property(x => x.Fee).HasPrecision(10, 2);

            builder.HasOne(x => x.AppUsers).WithMany(x => x.Transactions).HasForeignKey(x => x.UserId);
        }
    }
}
