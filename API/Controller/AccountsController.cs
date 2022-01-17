using API.Base;
using API.Models;
using API.Repository.Data;
using API.View_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, int>
    {
        private readonly AccountRepository accountRepository;
        public IConfiguration _configuration;
        public AccountsController(AccountRepository accountRepository,IConfiguration configuration) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            this._configuration = configuration;

        }

        [HttpPost]
        [Route("Login")]
        public ActionResult LoginPage(LoginVM loginVM)
        {
            
            try
            {
                var result = accountRepository.LoginPage(loginVM);
                if (result == 1)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result, message = "Data not Found" });
                }
                else if (result == 2)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result, message = " Wrong Password  " });

                }
                else if (result == 3)
                {
                    var tokenPayload = accountRepository.tokenPayload(loginVM.Email);
                    var claims = new List<Claim>
                    {
                        new Claim("email", loginVM.Email)
                        //new Claim("roles", data.role)
                    };
                    foreach (var role in tokenPayload.Role)
                    {
                        claims.Add(new Claim("roles", role));
                    }
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //header
                    var token = new JwtSecurityToken(
                                _configuration["Jwt:Issuer"],
                                _configuration["Jwt:Audience"],
                                claims,
                                expires: DateTime.UtcNow.AddMinutes(5),
                                signingCredentials: signIn
                        );
                    var idtoken = new JwtSecurityTokenHandler().WriteToken(token); //Generate Token
                    claims.Add(new Claim("TokenSecurity : ", idtoken.ToString()));
                    return Ok(new JWTtokenVM {status = HttpStatusCode.OK, tokenId = idtoken, message ="Login Successfully" } );
                }
                else
                {
                    return Ok(new JWTtokenVM { status = HttpStatusCode.BadRequest, tokenId = null, message = "error" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest});
            }
        }

        [HttpPost]
        [Route("ForgotPassword")]

        public ActionResult ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            var checkEmail = accountRepository.ForgotPassword(forgotPasswordVM.Email);
            if (checkEmail == 1)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, checkEmail, message = "Email not Found" });
            }
            else if (checkEmail == 2)
            {
                return Ok(new { status = HttpStatusCode.OK, checkEmail, message = "Check Your Email Adress" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest });
            }
        }

        [HttpPost]
        [Route("changepassword")]

        public ActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            try { 
            //cara 1
            var change = accountRepository.ChangePassword(changePasswordVM);
            if (change == 1)
            {
                return Ok(new { status = HttpStatusCode.OK, change, message = "Password successfully changed" });
            }
            else if (change == 2)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, change, message = " Expired OTP, Request OTP Again" });
            }
            else if (change == 3)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, change, message = " OTP already used" });
            }
            else if (change == 4)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, change, message = " Failed OTP" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, change, message = " Unkwon Error " });
            }
            }
            catch (Exception e)
            {
                return BadRequest(new { status = e.Message });
            }

            //var change = accountRepository.ChangePassword(changePasswordVM);
            //if (change == 1)
            //{
            //    return Ok(new { status = HttpStatusCode.OK, change, message = "Password changed" });
            //}
            //else if (change == 2)
            //{
            //    return BadRequest(new { status = HttpStatusCode.BadRequest, change, message = " Failed Change Passowrd" });

            //}
            //else if (change == 3)
            //{
            //    return BadRequest(new { status = HttpStatusCode.BadRequest, change, message = " Failed OTP" });

            //}
            //else if (change == 3)
            //{
            //    return BadRequest(new { status = HttpStatusCode.BadRequest, change, message = " Email not Found" });
            //}
            //else
            //{
            //    return BadRequest(new { status = HttpStatusCode.BadRequest });
            //}


        }
        
        [HttpGet("TestJWT")]
        public ActionResult TestJWT()
        {
            return Ok("Test JWT Berhasil");
        }


        [Authorize(Roles = "Employee")]
        [HttpGet("TestJWTEmployee")]
        public ActionResult TestJWTEmployee()
        {
            return Ok("Test JWT Employee Berhasil");
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("TestJWTManager")]
        public ActionResult TestJWTManager()
        {
            return Ok("Test JWT Manager Berhasil");
        }

        [Authorize(Roles = "Director")]
        [HttpGet("TestJWTDirector")]
        public ActionResult TestJWTDirector()
        {
            return Ok("Test JWT Director Berhasil");
        }
    }
}









//        //coba
//        public ActionResult LoginPage(RegisterVM registerVM)
//        {

//            try
//            {
//                var result = accountRepository.LoginPage(registerVM);
//                if (result == 1)
//                {
//                    return BadRequest(new { status = HttpStatusCode.BadRequest, result, message = "Data not Found" });
//                }


//                else if (result == 2)
//                {
//                    return BadRequest(new { status = HttpStatusCode.BadRequest, result, message = " Wrong Password  " });

//                }

//                else if (result == 3)
//                {
//                    return Ok(new { status = HttpStatusCode.OK, result, message = " Login Successfully  " });

//                }
//                else
//                {
//                    return BadRequest(new { status = HttpStatusCode.BadRequest, result, message = " Error" });
//                }
//            }
//            catch (Exception)
//            {
//                return BadRequest(new { status = HttpStatusCode.BadRequest });
//            }
//        }

//    }
//}


