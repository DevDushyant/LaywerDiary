using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_work_master_sub", Schema = "ld")]
    public class WorkMasterSubEntity : AuditableEntity
    {
        public new Guid Id { get; set; }
        [Required]
        public Guid WMasterId { get; set; }
        [Required]
        public required string Name_En { get; set; }
        public string Name_Hn { get; set; }
        [Required]
        [ForeignKey("WMasterId")]
        public virtual WorkMasterEntity WorkMaster { get; set; }
    }
}
