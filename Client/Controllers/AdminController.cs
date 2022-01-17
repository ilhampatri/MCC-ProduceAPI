using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Chart()
        {
            return View();
        }
        public IActionResult TableEmployee()
        {
            return View();
        }
        public IActionResult Pokemon()
        {
            return View();
        }

    }
}
