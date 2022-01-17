using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_university")]
    public class University
    {
        public int UniversityId { get; set; }
        public string UniversityName { get; set; }

        //[JsonIgnore]

        public virtual ICollection<Education> Educations { get; set; }
    }
}
