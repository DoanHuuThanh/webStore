using doanhuuthanh_web.Application.Catalog.Products;
using doanhuuthanh_web.Application.common;
using doanhuuthanh_web.Application.System.Users;
using doanhuuthanh_web.Data.EF;
using doanhuuthanh_web.Data.Entities;
using doanhuuthanh_web.ViewModel.System.Users;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore;

namespace doanhuuthanh_web.backendApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddHttpClient();

            // Add services to the container.
            builder.Services.AddDbContext<doanhuuthanhDbContext>(options => options.UseSqlServer(
              builder.Configuration.GetConnectionString("doanhuuthanh_webDb")));
            builder.Services.AddIdentity<AppUser, AppRole>() // được sử dụng để cấu hình và đăng ký các dịch vụ liên quan đến quản lý người dùng và vai trò (Identity) trong ứng dụng ASP.NET Core.
                            .AddEntityFrameworkStores<doanhuuthanhDbContext>()//Phương thức này chỉ định rằng dữ liệu về người dùng và vai trò sẽ được lưu trữ trong cơ sở dữ liệu thông qua Entity Framework Core
                            .AddDefaultTokenProviders();//nó cung cấp các cài đặt mặc định cho việc tạo và xác minh các token (ví dụ: token xác minh email, token đặt lại mật khẩu) trong quá trình quản lý tài khoản người dùng.

            builder.Services.AddTransient<IStorageService,FileStorageService>();
            builder.Services.AddTransient<IPublicProductService, PublicProductService>();
            builder.Services.AddTransient<IManageProductService, ManageProductService>();
            builder.Services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();    
            builder.Services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
            builder.Services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();    
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IValidator<LoginRequest>, LoginRequestValidation>();
            builder.Services.AddTransient<IValidator<RegisterRequest>, RegisterRequestValidation>();

            //AddFluentValidation để cấu hình việc sử dụng FluentValidation trong các Controller. 
            //RegisterValidatorsFromAssemblyContaining<LoginRequestValidation>() cho biết rằng FluentValidation nên sử dụng các validators được định nghĩa trong cùng assembly chứa LoginRequestValidation.
            builder.Services.AddControllers().AddFluentValidation(fv=> fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidation>());
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "doan huu thanh web", Version = "v1" });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","Swagger DoanHuuThanh V1");
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}