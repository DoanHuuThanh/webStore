﻿using doanhuuthanh_web.addminWeb.Services;
using doanhuuthanh_web.ViewModel.System.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace doanhuuthanh_web.addminWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserApiClient _userApiClient;
        public UserController(IUserApiClient userApiClient)
        {

            _userApiClient = userApiClient;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if(!ModelState.IsValid)
            {
                return View(ModelState);
            }
            var token = await _userApiClient.Authencate(request);

            return View();
        }

    }
}
