using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_tr_profilling")]
    public class Profilling
    {
        [Key]
        public string NIK { get; set; }
        public virtual Education Education { get; set; }
        public int EducationId { get; set; }
        public virtual Account Account { get; set; }
        
    }
}
