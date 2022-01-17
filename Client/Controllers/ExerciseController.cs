using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ExerciseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
