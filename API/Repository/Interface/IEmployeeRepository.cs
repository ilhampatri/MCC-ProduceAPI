using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
    interface IEmployeeRepository
    {
        IEnumerable<Employee> Get();
        //Get all semua data sama seperti SELECT * FROM, //IEnumerable seperti penampung , misal ada 5 data , mka 5 data akan di tampung , 
        //Bisa pakai IList, dengan bentuk data seperti List, Cocok jika menggunakan list atau manggil data list, atau update data ke dalam list
        //IList<Employee> Get();
        //Jika hanya read data lebih cocok menggunakan IEnumerable
        Employee Get(string NIK); // fungsinya bisa return data dari employee , tapi hanya NIK karna PK, sebaiknya PK// hanya mengambil 1 row berdasarkan NIK bertipe NON VOID
        int Insert(Employee employee);
        int Update(string NIK,Employee employee);
        int Delete(string NIK);
    }
}

//detached untuk melepaskan data