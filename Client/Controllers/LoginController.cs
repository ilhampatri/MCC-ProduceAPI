using API.Models;
using API.View_Models;
using Client.Base;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{


    public class LoginController : BaseController<Account, LoginRepository, string>
    {
        //private readonly LoginRepository repository;
        private readonly LoginRepository repository;
        public LoginController(LoginRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        public IActionResult index()
        {
            return View();
        }

        [HttpPost("Login/Auth")]
        public async Task<IActionResult> Auth(LoginVM login)
        {
            var jwtToken = await repository.Auth(login);
            var token = jwtToken.tokenId;

            if (token == null)
            {
                TempData["message"] = jwtToken.message;
                return RedirectToAction("index", "Login");
            }

            HttpContext.Session.SetString("JWToken", token);

            return RedirectToAction("Dashboard", "Admin");
        }

        [HttpGet("Logout/")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index", "Home");
        }
        [HttpGet("notfound/")]
        public IActionResult notfound()
        {
            return View("404");
        }
        [HttpGet("unauthorized/")]
        public IActionResult unauthorized()
        {
            return View("401");
        }

        [HttpGet("forbidden/")]
        public IActionResult forbidden()
        {
            return View("403");
        }
    }
}
