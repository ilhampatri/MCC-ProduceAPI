using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.View_Models
{
    public class ChangePasswordVM
    {
       //public string nik { get; set; }
        public string Email { get; set; }
        public int OTP { get; set; }
        public string NewPassword { get; set; }
    }
}
