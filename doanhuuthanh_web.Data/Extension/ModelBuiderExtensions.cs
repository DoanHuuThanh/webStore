using doanhuuthanh_web.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doanhuuthanh_web.Data.Extension
{
    public static class ModelBuiderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //Data seeding

            modelBuilder.Entity<AppConfig>().HasData(
                new AppConfig() { Key = "HomeTitle", Value = "This is home page of doanhuuthanh_web" },
                new AppConfig() { Key = "HomeKeyword", Value = "This is keyword of doanhuuthanh_web" },
                new AppConfig() { Key = "HomeDescription", Value = "This is description of doanhuuthanh_web" }
                );

            modelBuilder.Entity<Languages>().HasData(
                new Languages() { id = "vi-VN", name = "Việt Nam", isDefault = true },
                new Languages() { id = "en-US", name = "Mỹ", isDefault = false }

                );

            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    IsShowOnHome = true,
                    parentId = null,
                    SortOder = 1,
                    Status = Enums.Statuscs.Active,

                },
                new Category()
                {
                    Id = 2,
                    IsShowOnHome = true,
                    parentId = null,
                    SortOder = 2,
                    Status = Enums.Statuscs.Active,
                });

            modelBuilder.Entity<CategoryTranslation>().HasData(
                       new CategoryTranslation() { Id = 1, CategoryId = 1, Name = "Áo nữ", LanguageId = "vi-VN", SeoAlias = "ao-nu", SeoDescription = "Sản phẩm thời trang nu", SeoTitle = "Sản phẩm thời trang nu" },
                       new CategoryTranslation() { Id = 2, CategoryId = 1, Name = "Women Shirt", LanguageId = "en-US", SeoAlias = "women-shirt", SeoDescription = "The shirt products for women", SeoTitle = "The shirt products for women" },
                       new CategoryTranslation() { Id = 3, CategoryId = 2, Name = "Áo nam", LanguageId = "vi-VN", SeoAlias = "ao-nam", SeoDescription = "Sản phẩm thời trang nam", SeoTitle = "Sản phẩm thời trang nam" },
                       new CategoryTranslation() { Id = 4, CategoryId = 2, Name = "Men Shirt", LanguageId = "en-US", SeoAlias = "men-shirt", SeoDescription = "The shirt products for man", SeoTitle = "The shirt products for man" });


            modelBuilder.Entity<Product>().HasData(
               new Product()
               {
                   Id = 1,
                   DateCreated = DateTime.Now,
                   OriginalPrice = 1000000,
                   Price = 200000,
                   Stock = 0,
                   ViewCount = 0,

               });

            modelBuilder.Entity<ProductTranslation>().HasData(
                new ProductTranslation()
                {
                    Id = 1,
                    ProductId = 1,
                    Name = "Áo sơ mi nam trắng",
                    LanguageId = "vi-VN",
                    SeoAlias = "ao-so-mi-nam-trang",
                    SeoDescription = "Áo sơ mi nam trắng",
                    SeoTitle = "Áo sơ mi nam trắng",
                    Details = "Mô tả sản phẩm",
                    Description = "Áo sơ mi nam trắng"
                },
                       new ProductTranslation()
                       {
                           Id = 2,
                           ProductId = 1,
                           Name = "Men T-shirt",
                           LanguageId = "en-US",
                           SeoAlias = "men-t-shirt",
                           SeoDescription = "Men T-shirt",
                           SeoTitle = "Men T-shirt",
                           Details = "Mô tả sản phẩm",
                           Description = "Men T-shirt"
                       });

            modelBuilder.Entity<ProductInCategory>().HasData(new ProductInCategory() { CategoryId = 1, ProductId = 1 });


            // any guid, but nothing is against to use the same one

            var roleId = new Guid("C5AFBA34-161C-4F72-8D63-1E6514C07924");
            var adminId = new Guid("1E103484-14E2-4083-BF1A-7E44301ED0D7");


            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "thanh@gmail.com",
                NormalizedEmail = "thanh@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "123456"),
                SecurityStamp = string.Empty,
                FirstName = "Thanh",
                LastName = "Doan Huu",
                Dob = new DateTime(2002, 2, 23)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
            // cần thêm câu lệnh add-migration SeedData 
            //Khi bạn chạy lệnh Add-Migration SeedData, Entity Framework sẽ tạo ra một tệp migration mới mà bạn sau đó có thể áp dụng bằng cách sử dụng
            //Update-Database để thêm dữ liệu mà bạn đã định nghĩa trong phương thức Seed của ModelBuilderExtensions vào cơ sở dữ liệu.

            //Sau khi migration đã được áp dụng, dữ liệu seeding mà bạn đã định nghĩa trong phương thức Seed sẽ được thêm vào cơ sở dữ liệu của bạn.



        }
    }
}
