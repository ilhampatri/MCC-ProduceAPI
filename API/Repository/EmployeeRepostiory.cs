
using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace API.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        //querry2 akan berada di repository, querry berupa Entity Framework menggunakan syntax LINQ
        private readonly MyContext myContext;
        public EmployeeRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public int Delete(string NIK)
        {
            // throw new NotImplementedException();
            var entity = myContext.Employees.Find(NIK);
            myContext.Remove(entity);
            var hasil = myContext.SaveChanges();
            return hasil;
        } 

        public IEnumerable<Employee> Get()
        {
            return myContext.Employees.ToList(); // get data from employee
        }


        public Employee Get(string NIK)
        {
            return myContext.Employees.Where(e => e.NIK == NIK).FirstOrDefault();


            //throw new NotImplementedException();
            //untuk mengambil data 1 row
            //return myContext.Employees.Find(NIK); // Atribut yang dilempar harus merupakan PK, jika Non akan error
            //return myContext.Employees.Where(e => e.NIK==NIK).SingleOrDefault();
            //single or default hanya bisa menampilkan data yang memiliki 1 nama, jika ada duplikat akan terjadi error,
            //solusi untuk menampilkandua duanya adalah firstor default dan hanya menampilkan yang pertama saja
            //where penggunaannya sama seperti SQL
            //e == alias, e.NIK mewakili tabel employee
        }

        public int Insert(Employee employee)
        {
            var checkNIK = myContext.Employees.Any(x => x.NIK == employee.NIK); //checkdata merupakan representasi dari database
            var checkphone = myContext.Employees.Any(x => x.Phone == employee.Phone);
            var checkemail = myContext.Employees.Any(x => x.Email == employee.Email);
            if (checkNIK)
            {
                return 2;
            }
            else if (checkphone)
            {
                return 3;
            }
            else if (checkemail)
            {
                return 4;
            }
            else
            {
                myContext.Employees.Add(employee);
                var respon = myContext.SaveChanges(); //savechanges tipe datanya berupa int ,makanya insert delet update berupa int
                return 0;
            }
        }
        //myContext.Employees.Add(employee);
        //var respon = myContext.SaveChanges(); //savechanges tipe datanya berupa int ,makanya insert delet update berupa int
        //return respon;
        //masih error , bisa jadi solusi mebuat var untuk masing2 check

        // return myContext.SaveChanges(); bisa juga dengan ini
        //throw new NotImplementedException();

        public int Update(string NIK, Employee employee)
        {
            //var checkData = myContext.Employees.AsNoTracking().Where(e => e.NIK == NIK).FirstOrDefault();
            var checkData = myContext.Employees.AsNoTracking().FirstOrDefault(e => e.NIK == NIK);
            //if (checkData.Email == employee.Email || myContext.Employees.FirstOrDefault(e => e.Email == employee.Email) == null)
            //if (checkData.Phone == employee.Phone || myContext.Employees.FirstOrDefault(e => e.Phone == employee.Phone) == null)
            if (checkData != null)
            {
                myContext.Entry(employee).State = EntityState.Detached;
                var checkPhone = myContext.Employees.Where(e => e.Phone == employee.Phone).FirstOrDefault();
                if (checkPhone == null || checkData.Phone == employee.Phone)
                //if (checkData.Email == employee.Email || myContext.Employees.FirstOrDefault(e => e.Email == employee.Email) == null)
                {
                    var checkEmail = myContext.Employees.Where(e => e.Email == employee.Email).FirstOrDefault();
                    //if (checkData.Phone == employee.Phone || myContext.Employees.FirstOrDefault(e => e.Phone == employee.Phone) == null)

                    if (checkEmail == null || checkData.Email == employee.Email)
                    {
                        employee.NIK = NIK;
                        myContext.Entry(employee).State = EntityState.Modified;
                        var result = myContext.SaveChanges();
                        return result;
                    }
                    else
                    {
                        return 4;
                    }
                }
                else
                {
                    return 3;
                }
            }
            else
            {
                return 0;
            }


        }
        IEnumerable<Employee> IEmployeeRepository.Get()
        {
            throw new NotImplementedException();
        }

        Employee IEmployeeRepository.Get(string NIK)
        {
            throw new NotImplementedException();
        }
    }
}
