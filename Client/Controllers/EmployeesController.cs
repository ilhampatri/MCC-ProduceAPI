using API.Models;
using API.View_Models;
using Client.Base;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    // [Authorize]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository repository;

        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<JsonResult> GetRegistered()
        {
            var result = await repository.GetRegistered();
            return Json(result);
        }
        [HttpGet("Employees/GetRegistered/{nik}")]

        public async Task<JsonResult> GetRegisteredByNik(string nik)
        {
            var result = await repository.GetRegisteredByNik(nik);
            return Json(result);
        }

        [HttpPost("Employees/Register")]
        public JsonResult Register(RegisterVM entity)
        {
            var result = repository.Register(entity);
            return Json(result);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
