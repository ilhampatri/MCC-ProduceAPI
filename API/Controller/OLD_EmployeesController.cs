
using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]

    public class OLD_EmployeesController : ControllerBase
    {


        private readonly EmployeeRepository employeeRepository;
        public OLD_EmployeesController(EmployeeRepository employeeRepository)
        {

            this.employeeRepository = employeeRepository;
        }
        [HttpPost]
        public ActionResult Insert(Employee employee)
        {
            try
            {
            var insert = employeeRepository.Insert(employee);
            if (insert == 0)
            {
                return Ok(new { status = HttpStatusCode.OK, insert, message = "data successfully entered" });
            }
            else if (insert == 2)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, insert, message = "NIK already exist" });
            }
            else if(insert == 3)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, insert, message = "Phone already exist" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, insert, message = "Email already exist" });
            }
            }
            catch
            {
                throw;
            }

            /*try
            {
                var insert = employeeRepository.Insert(employee);
                if (insert > 0)
                {
                    return Ok(new { status = HttpStatusCode.OK, insert, massage = "Berhasil input data" });
                }
                else
                {
                    return Ok(new { status = HttpStatusCode.NotFound, massage = "Gagal memasukkan data" });
                }
                //return Ok(employeeRepository.Insert(employee));
            }
            catch
            {
                throw;
            }
            */
        }
        [HttpGet]
        public ActionResult Get()
        {
            var result = employeeRepository.Get().Count();
            var show = employeeRepository.Get();
            if (result != 0)
            {
                //employeeRepository.Get();
                return Ok(new { status = HttpStatusCode.OK,show, massage = "successfully display data" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result, massage = "Empty data" });
            }
            //return Ok(employeeRepository.Get());
        }
        //get 1 NIK
        [HttpGet("{NIK}")]
        //[route] ??
        public ActionResult Get(string NIK)
        {
            try
            {
                var result = employeeRepository.Get(NIK);
                if (result != null)
                {
                    return Ok(new { status = HttpStatusCode.OK, result, message = "Data Found" });
                }
                else
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result, message = "Data not Found" });
                }
            }
            catch
            {
                throw;
            }
        }
        //Update
        [HttpPut("{NIK}")]
        public ActionResult Update(string NIK,Employee employee)
        {
            var update = employeeRepository.Update(NIK,employee);
            if (update == 1)
            {
                return Ok(new { status = HttpStatusCode.OK, update, message = "data successfully updated" });
            }
            else if (update == 2)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, update, message = "NIK already exist" });
            }
            else if (update == 3)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, update, message = "Phone already exist" });
            }
            else if (update == 4)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, update, message = "Email already exist" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, update, message = "Update fail" });
            }
            /*
            var code = 0;
            var message = "";
            try
            {
                var result = employee;
                if (result != null)
                {
                    code = StatusCodes.Status200OK;
                    employeeRepository.Update(employee);
                    message = "data successfully changed";
                }
                else
                {
                    code = StatusCodes.Status404NotFound;
                    message = "Employee NIK found";
                }
                string jsonString = JsonConvert.SerializeObject(new { code, result, message });
                return Ok(jsonString);
            }
            catch
            {
                throw;
            }
            //return Ok(employeeRepository.Update(employee));
            */
        }
        //Hapus 1 NIK doang
        [HttpDelete("{NIK}")]
        public ActionResult hapus(string NIK)
        {
            try
            {
                var result = employeeRepository.Get().Count();
                if (result != 0)
                {
                    employeeRepository.Delete(NIK);
                    return Ok(new { status = HttpStatusCode.OK, result, message = "Successfully delete data" });
                }
                else
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result, message = "NIK not Found" });
                }
            }
            catch
            {
                throw;
            }
        }


    }
}
