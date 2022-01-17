using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_education")]
    public class Education
    {
        public int EducationId { get; set; }
        public string Degree { get; set; }
        public float GPA { get; set; }
        public virtual University University { get; set; }
        public int UniversityId { get; set; }

        public ICollection<Profilling> Profillings { get; set; }
        //public virtual ICollection<Profilling> Profillings { get; set; }.
    }
}
