    using doanhuuthanh_web.Data.Entities;
    using doanhuuthanh_web.Utilities;
    using doanhuuthanh_web.ViewModel.System.Users;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    namespace doanhuuthanh_web.Application.System.Users
    {
        public class UserService : IUserService
        {
        private readonly UserManager<AppUser> _userManager; //Lớp quản lý người dùng. Được sử dụng để tìm kiếm người dùng, xác thực mật khẩu, tạo người dùng, v.v.
        private readonly SignInManager<AppUser> _signInManager; //Lớp quản lý đăng nhập. Được sử dụng để đăng nhập và đăng xuất người dùng.
        private readonly RoleManager<AppRole> _roleManager; // Lớp quản lý vai trò. Được sử dụng để quản lý vai trò của người dùng.
        private readonly IConfiguration _config; 
            public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager,IConfiguration configuration) { 
                _userManager = userManager;
                _signInManager = signInManager;
                _roleManager = roleManager;
                _config = configuration;
           }
            public async Task<string> Authencate(LoginRequest request)
            {
                var user = await _userManager.FindByNameAsync(request.UserName); // tìm kiếm người dùng
                if (user == null)
                {
                    throw new webException("Cannot file UserName");
                }
                var roles = await _userManager.GetRolesAsync(user); // lấy ra danh sách vai trò của user

                var result = await _signInManager.PasswordSignInAsync(user,request.Password,request.RememberMe,true); // đăng nhập người dùng sử dụng tên người dùng và mật khẩu
                if(!result.Succeeded) { return null; }
                var claims = new[]  
                {
                    new Claim(ClaimTypes.Email,user.Email),// ClaimTypes.Email được tạo ra với giá trị là địa chỉ email của người dùng.
                    new Claim(ClaimTypes.GivenName,user.FirstName), //ClaimTypes.GivenName: Đây cũng là một "claim" chuẩn, thường được sử dụng để biểu thị tên của người dùng.
                    new Claim(ClaimTypes.Role,string.Join(";",roles))//ClaimTypes.Role: Đây có thể là một "claim" tự định nghĩa, dùng để biểu thị quyền hạn của người dùng
                    
                };
              //tạo key
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
              // tạo chữ kĩ bằng thuật toán hacsha256
                var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            //tạo jwt
                var token = new JwtSecurityToken(
                    issuer:"admin",
                    audience:user.UserName,
                    claims,
                    expires:DateTime.Now.AddHours(3),
                    signingCredentials:creds);

               return new JwtSecurityTokenHandler().WriteToken(token); // cách tạo chuỗi từ jwt đã tạo


            }   

            public async Task<bool> Register(RegisterRequest request)
            {
                var user = new AppUser()
                {
                    Dob = request.Dob,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    UserName = request.UserName,
                    PhoneNumber = request.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user,request.Password);
                if(result.Succeeded)
                {
                    return true;
                }
                return false;
            }
        }
    }
