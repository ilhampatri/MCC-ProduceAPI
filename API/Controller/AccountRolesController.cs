
using API.Base;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
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
    public class AccountRolesController : BaseController<AccountRole, AccountRoleRepository, int>
    {
        private readonly AccountRoleRepository accountRoleRepository;

        public AccountRolesController(AccountRoleRepository accountRoleRepository) : base(accountRoleRepository)
        {
            this.accountRoleRepository = accountRoleRepository;
        }
        [Authorize(Roles = "Director")]
        [HttpGet("signmanager/{nik}")]
        public ActionResult SignManager(string nik)
        {
            var assign = accountRoleRepository.SignManager(nik);
            if (assign == 2)
            {
                return Conflict(new { status = "Failed", message = "This account already to be manager" });
            }
            if (assign == 1)
            {
                return Ok(new { status = HttpStatusCode.OK, message = "This account registered as manager " });
            }
            else
            {
                return BadRequest(new { status =HttpStatusCode.BadRequest, message = "Error" });
            }
        }
    }
}
