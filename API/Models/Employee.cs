using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{

    // cara untuk merubah nama tabel di smss
    [Table("tb_m_employee")]
    public class Employee
    {
        //atribut : NIK sebagai PK, firstname,last name, phone, birthdaye. salary,email, gender
        //auto PK jika pada PK membuat nama atribut menjadi namamodel_id/ namamodelid
        //[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [Key] //fungsi dari [key] ini untuk menentukan atribut yang tepat dibawah key menjadi PK
        public string NIK { get; set; }
        [StringLength(20,ErrorMessage ="Maximal 20 character")]
        //public string FullName { get; set; }
        public string FirstName { get; set; }
        [MinLength(2, ErrorMessage = "Minimal 2 character")]
        [StringLength(20, ErrorMessage = "Maximal 20 character")]
        public string LastName { get; set; }
        
        [Required] //data harus diisi jika memakai required
        [Phone(ErrorMessage = "Watch what you write.")] // perhatikan format penulisan nomor hp misal tidak bisa menggunakan */ dll
        public string Phone { get; set; }
        //[DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Range(5000000,12000000,ErrorMessage ="Input between 5000000 - 12000000")]
        //DataType (DataType.EmailAddress) )berguna untuk presentasi data)tidak dapat digunakan untuk memvalidasi input pengguna..
        //Ini hanya digunakan untuk memberikan petunjuk UI untuk memberikan nilai menggunakan templat tampilan / editor.
        public int Salary { get; set; }
       [EmailAddress(ErrorMessage = "Watch what you write.")] //perhatikan format penulisa dari email
        public string Email { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; }//satu untuk manggil method atau type, satu sebagai atribut
        //[JsonIgnore]
        public virtual Account Account { get; set; }
    }
    public enum Gender
    {
        Male,
        Female,
    }
}
