using API.Context;
using API.Models;
using API.View_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext myContext;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public int Register(RegisterVM registerVM)
        {
            
            //string year = DateTime.Now.Year.ToString();
            string year = DateTime.Now.Year.ToString("yyyy");

            //var countEmployee = myContext.Employees.ToList().Count + 1;

            int countEmployee = myContext.Employees.ToList().Count;
            string formattedNIK = "";
            if (countEmployee == 0)
            {
                formattedNIK = year + "0" + countEmployee.ToString();
            }
            else
            {
                string increament2 = myContext.Employees.ToList().Max(e => e.NIK);
                int formula = Int32.Parse(increament2) + 1;
                formattedNIK = formula.ToString();

            }

            //var formattedNIK = year + "0" + countEmployee;
            var checkPhone = myContext.Employees.Where(emp => emp.Phone == registerVM.Phone).FirstOrDefault();
            var checkEmail = myContext.Employees.Where(emp => emp.Email == registerVM.Email).FirstOrDefault();
            if ( checkEmail != null && checkPhone != null)
            {
                return 1;
            }
            else if (checkPhone != null)
            {
                return 2;
            }
            else if (checkEmail != null)
            {
                return 3;
            }
            else
            {
                var employee = new Employee
                {
                    NIK = formattedNIK,
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    Phone = registerVM.Phone,
                    BirthDate = registerVM.Birthdate,
                    Gender = (Models.Gender)registerVM.Gender,
                    Salary = registerVM.Salary,
                    Email = registerVM.Email
                };
                myContext.Employees.Add(employee);
                myContext.SaveChanges();

                string hashPassword = BCrypt.Net.BCrypt.HashPassword(registerVM.Password);
                var account = new Account
                {
                    NIK = employee.NIK,
                    Password = hashPassword
                };
                myContext.Accounts.Add(account);
                myContext.SaveChanges();

                var accountRole = new AccountRole
                {
                    AccountId = employee.NIK,
                    RoleId = 3
                };
                myContext.AccountRoles.Add(accountRole);
                myContext.SaveChanges();

                var education = new Education
                {
                    Degree = registerVM.Degree,
                    GPA = registerVM.GPA,
                    UniversityId = registerVM.UniversityId
                };
                myContext.Educations.Add(education);
                myContext.SaveChanges();

                var profilling = new Profilling
                {
                    NIK = employee.NIK,
                    EducationId = education.EducationId
                };
                myContext.Profillings.Add(profilling);
                myContext.SaveChanges();
                
                return 0;
                
            }
        }
           
        public IEnumerable<object> GetRegisterData()
        {
            var result = from employees in myContext.Employees
                         join accounts in myContext.Accounts
                            on employees.NIK equals accounts.NIK
                         join profilings in myContext.Profillings
                            on accounts.NIK equals profilings.NIK
                         join educations in myContext.Educations
                            on profilings.EducationId equals educations.EducationId
                         join universities in myContext.Universities
                            on educations.UniversityId equals universities.UniversityId

                         
                         //join profilingEdId in myContext.Set<Profiling>()
                         //   on educations.EducationId equals profilingEdId.EducationId

                         select new
                         {
                             NIK = employees.NIK,
                             FullName = employees.FirstName + " " + employees.LastName,
                             FirstName = employees.FirstName,
                             LastName = employees.LastName,
                             Gender = employees.Gender,
                             Phone = employees.Phone,
                             BirthDate = employees.BirthDate,
                             Salary = employees.Salary,
                             Email = employees.Email,
                             Degree = educations.Degree,
                             GPA = educations.GPA,
                             University = universities.UniversityName
                         };
            return result;
        }

        public RegisteredVM GetRegisteredByNIK(string nik)
        {
            var result = myContext.Employees.Where(e => e.NIK == nik)
                .Include(e => e.Account)
                .ThenInclude(e => e.Profilling)
                .ThenInclude(e => e.Education)
                .ThenInclude(e => e.University)
                .FirstOrDefault();


            //join profilingEdId in myContext.Set<Profiling>()
            //   on educations.EducationId equals profilingEdId.EducationId
            if (result == null)
            {
                return null;
            }
            var selectedData = new RegisteredVM
            {
                NIK = result.NIK,

                FullName = result.FirstName + " " + result.LastName,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Gender = result.Gender,
                Phone = result.Phone,
                Birthdate = result.BirthDate,
                Salary = result.Salary,
                Email = result.Email,
                Degree = result.Account.Profilling.Education.Degree,
                GPA = result.Account.Profilling.Education.GPA,
                University = result.Account.Profilling.Education.University.UniversityName
            };
            return selectedData;


        }

        //Eager loading 
        public IEnumerable<Object> GetRegisteredDataEagerly()
        {
            var eagerloading = myContext.Employees
                .Include(ac => ac.Account)
                .ThenInclude(p => p.Profilling)
                .ThenInclude(e => e.Education)
                .ThenInclude(u => u.University);
            return eagerloading;

        }
        public IEnumerable SalaryStat()
        {
            var result = (from e in myContext.Employees
                          group e by new { e.Salary } into Group
                          select new
                          {
                              Salary = Group.Key.Salary,
                              ValueTask = Group.Count()
                          });

            return result.ToList();
        }
        public IEnumerable GetGenderStat()
        {
            var query = (from employee in myContext.Set<Employee>()
                         group employee by employee.Gender into g
                         select new
                         {
                             gender = g.Key,
                             count = g.Count()
                         });
            return query.ToList();
        }
    }
}
