using API.Base;
using API.Models;
using API.Repository.Data;
using API.View_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController < Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeesController(EmployeeRepository employeeRepository) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpPost]
        [Route("Register")]

        public ActionResult Register(RegisterVM registerVM)
        {
            try
            {
                var result = employeeRepository.Register(registerVM);
                if (result == 1)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result, message = "Email and Phone already used" });
                }
                else if ( result == 2)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result, message = "Phone already used" });

                }
                else if (result == 3)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result, message = "Email already used" });

                }
                else 
                {
                    
                    return Ok(new { status = HttpStatusCode.OK, result, message = " Data inserted" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { status = e.Message});
            }
        }
        [HttpGet]
        [Route("Register")]

        public ActionResult<RegisterVM> GetRegisterData()
        {
            try 
            {
               var result = employeeRepository.GetRegisterData(); //complex query operators

                //var result = employeeRepository.GetRegisteredDataEagerly(); ; //eager loading

                //var result = employeeRepository.GetRegisterData().ToList(); //lazy loading
                if(result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound(new { status = HttpStatusCode.NotFound, result, message = "No Data Found" });
                }
                //return Ok(result);
            }
            catch( Exception)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest });
            }
        }

        [HttpGet]
        [Route("Register/{nik}")]

        public ActionResult<RegisteredVM> GetRegisteredByNIK(string nik)
        {
            try
            {
                var result = employeeRepository.GetRegisteredByNIK(nik); //complex query operators

                //var result = employeeRepository.GetRegisteredDataEagerly(); ; //eager loading

                //var result = employeeRepository.GetRegisterData().ToList(); //lazy loading
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound(new { status = HttpStatusCode.NotFound, result, message = "No Data Found" });
                }
                //return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest });
            }
        }

        [HttpGet("TestCORS")]
        public ActionResult TestCors()
        {
            return Ok("Test CORS Berhasil");
        }

        [HttpGet("SalaryStat")]
        public ActionResult SalaryStat()
        {
            var result = employeeRepository.SalaryStat();
            return Ok(result);
        }
        [HttpGet("GenderStat")]
        public ActionResult GenderStat()
        {
            try
            {
                var data = employeeRepository.GetGenderStat();
                if (data == null)
                {
                    return Ok(new { status = "success", data = "no data found" });
                }

                return Ok(new { status = "success", data = data });
            }
            catch (Exception e)
            {
                return BadRequest(new { status = "failed", message = e });
            }
        }
    }
    
}
