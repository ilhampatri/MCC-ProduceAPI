using API.Context;
using API.Models;
using API.View_Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, int>
    {
        private readonly MyContext myContext;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public RoleVM tokenPayload(string email)
        {
            var getUserData = (from e in myContext.Employees
                               where e.Email == email
                               join a in myContext.Set<Account>() on e.NIK equals a.NIK
                               join ar in myContext.Set<AccountRole>() on a.NIK equals ar.AccountId
                               join r in myContext.Set<Role>() on ar.RoleId equals r.RoleId
                               select new
                               {
                                   name = r.RoleName
                               }).ToList();
            List<string> roles = new List<string>();

            foreach (var item in getUserData)
            {
                roles.Add(item.name);
            }
            RoleVM payload = new RoleVM
            {
                Email = email,
                Role = roles
            };

            return payload;
        }

        public int ForgotPassword(string email)
        {
            var check = (from employees in myContext.Set<Employee>()
                         where employees.Email == email
                         join account in myContext.Set<Account>()
                         on employees.NIK equals account.NIK
                         select new
                         {
                             employees.Email,
                             account.NIK

                         }).FirstOrDefault();
            var checkEmail = check;
            if (checkEmail == null)
            {
                return 1;
            }
            else
            {
                //Error ketika memasukkan jika emailnya salah
                var thisAccount = myContext.Accounts.Where(acc => acc.NIK == checkEmail.NIK).FirstOrDefault();
                Random random = new Random();
                thisAccount.OTP = random.Next(100000, 999999);
                thisAccount.ExpiredToken = DateTime.Now.AddMinutes(0.5);
                thisAccount.isUsed = false;
                SentOTP(email, thisAccount.OTP, thisAccount.ExpiredToken);
                myContext.SaveChanges();
                return 2;
            }
        }
        public int SentOTP(string email, int OTP, DateTime ExpiredToken)
        {
            var message = new MimeMessage();
            MailboxAddress from = new MailboxAddress("Admin",
            "patriilham002@gmail.com");
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress("User",
            email);
            message.To.Add(to);

            message.Subject = "THIS IS YOUR OTP";

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = $"Please the OTP number is not given to anyone else. Immediately change the password before the token runs out. \nThis is your OTP:{OTP} \n Expired Date : {ExpiredToken}";

            message.Body = bodyBuilder.ToMessageBody();
            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            client.Authenticate("patriilham002@gmail.com", "ham981998");
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
            return 2;
        }
        public int LoginPage(LoginVM loginVM)
        {
            //FIX
            var check = (from employees in myContext.Set<Employee>()
                         where employees.Email == loginVM.Email
                         join account in myContext.Set<Account>()
                         on employees.NIK equals account.NIK
                         select new
                         {
                             employees.Email,
                             account.Password
                         }).FirstOrDefault();
            var checkdata = check;
            if (checkdata == null)
            {
                return 1;
            }
            bool validPassword = BCrypt.Net.BCrypt.Verify(loginVM.Password, checkdata.Password);
            if (!validPassword)
            {
                return 2;
            }
            else
            {
                return 3;
            }

        }

        //Cara 1
        public int ChangePassword(ChangePasswordVM changePasswordVM)
        {
            //var checkEmail = myContext.Employees.Where(e => e.Email == changePasswordVM.Email).SingleOrDefault();
            var checkEmail = myContext.Employees.SingleOrDefault(e => e.Email == changePasswordVM.Email);
            //if (checkEmail != null)
            if (changePasswordVM.Email != null)
            {
                var checkAccount = myContext.Accounts.SingleOrDefault(a => a.NIK == checkEmail.NIK);
                //var checkAccount = myContext.Accounts.Where(acc => acc.OTP == changePasswordVM.OTP).SingleOrDefault();
                if (checkAccount.OTP == changePasswordVM.OTP)
                {
                    if (checkAccount.isUsed == false)
                    {
                        if (checkAccount.ExpiredToken > DateTime.Now)
                        {
                            checkAccount.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordVM.NewPassword);
                            checkAccount.isUsed = true;
                            myContext.Entry(checkAccount).State = EntityState.Modified;
                            myContext.SaveChanges();
                            return 1;
                        }
                        else
                        {
                            return 2;
                        }
                    }
                    else
                    {
                        return 3;
                    }
                }
                else
                {
                    return 4;
                }
            }
            else
            {
                return 5;
            }
        }

    }

}



















    //    var employeeEmail = myContext.Employees.Where(e => e.Email == changePasswordVM.Email).FirstOrDefault();
    //    if (employeeEmail == null)
    //    {
    //        var checkAccount = myContext.Accounts.Where(acc => acc.OTP == changePasswordVM.OTP).FirstOrDefault();
    //        if (checkAccount == null)
    //        {
    //            if (checkAccount != null && employeeEmail != null)
    //            {
    //                checkAccount.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordVM.NewPassword);
    //                checkAccount.isUsed = true;
    //                myContext.Entry(checkAccount).State = EntityState.Modified;
    //                myContext.SaveChanges();
    //                return 1;
    //            }
    //            else
    //            {
    //                return 2;
    //            }
    //        }
    //        else
    //        {
    //            return 3;

    //        }
    //    }
    //    else
    //    {
    //        return 4;
    //    }
    //}




