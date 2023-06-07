using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("Mst_Lawyer", Schema = "LDiary")]
    public class LawyerMasterEntity : AuditableEntity
    {
        [Key]
        public new Guid Id { get; set; }
        [Required]
        public string name { get; set; }
        public int type { get; set; }
    }
}
