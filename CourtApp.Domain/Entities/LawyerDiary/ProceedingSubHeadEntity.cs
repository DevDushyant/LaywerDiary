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
        public Guid PHeadId { get; set; } 
        public required string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }        
        public virtual ProceedingHeadEntity ProceedingHead { get; set; }
    }
}
