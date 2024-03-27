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
    [Table("m_proceeding_sub_head", Schema = "ld")]
    public class ProceedingSubHeadEntity : AuditableEntity
    {
        public new Guid Id { get; set; }
        [Required]
        public Guid PHeadId { get; set; }        
        [Required]
        public required string Name_En { get; set; }
        public string Name_Hn { get; set; }
        [Required]
        [ForeignKey("PHeadId")]
        public virtual ProceedingHeadEntity ProceedingHead { get; set; }
    }
}
