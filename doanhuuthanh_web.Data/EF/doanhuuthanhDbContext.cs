using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using doanhuuthanh_web.Data.Entities;
using System.Security.Cryptography;
using doanhuuthanh_web.Data.Configurations;
using doanhuuthanh_web.Data.Extension;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace doanhuuthanh_web.Data.EF
{
    public class doanhuuthanhDbContext : IdentityDbContext<AppUser,AppRole,Guid>
    //DbContext cung cấp các phương thức và thuộc tính để thực hiện các hoạt động CRUD (Create, Read, Update, Delete) và quản lý các thay đổi trong cơ sở dữ liệu và quản lý tương tác với CSDL 
    {
        public doanhuuthanhDbContext(DbContextOptions options) : base(options)
        {
            //DbContextOptions trong Entity Framework Core là một lớp chứa các tùy chọn cấu hình cho lớp DbContext
            //cho phép bạn truyền các tùy chọn cấu hình vào DbContext của bạn, giúp xác định cách DbContext tương tác với cơ sở dữ liệu.


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            //configuration using Fluent API    
            modelBuilder.ApplyConfiguration(new AppConfigConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductInCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryTranslationConfiguration());
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new PromotionConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDeatilConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new ProductImageConfiguration());

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaim");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRole").HasKey(x=> new {x.UserId,x.RoleId});
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogin").HasKey(x=>x.UserId);

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaim");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserToken").HasKey(x=>x.UserId);

            modelBuilder.Seed();
            //base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; } // tương tự List khác là DbSet phản ánh trực tiếp với CSDL nó cung cấp các phương thức để truy vấn, thêm, sửa đổi và xóa dữ liệu
                                                      //DbSet<Products> cung cấp các phương thức và thuộc tính để truy vấn và thay đổi dữ liệu của bảng tương ứng trong cơ sở dữ liệu.
                                                      //Khi bạn thêm các đối tượng mới vào DbSet<Products>, Entity Framework Core sẽ giúp bạn thêm dữ liệu vào bảng tương ứng trong cơ sở dữ liệu.
                                                      //Tương tự, khi bạn truy vấn DbSet<Products>, bạn sẽ nhận được các đối tượng từ bảng tương ứng trong cơ sở dữ liệu.
        public DbSet<Category> Categories { get; set; }
        public DbSet<AppConfig> AppConfigs { get; set; }


        public DbSet<Cart> Carts { get; set; }

        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
        public DbSet<ProductInCategory> ProductInCategories { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Languages> Languages { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<ProductTranslation> ProductTranslations { get; set; }

        public DbSet<Promotion> Promotions { get; set; }


        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
    }
}
