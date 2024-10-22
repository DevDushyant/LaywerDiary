using AuditTrail.Abstrations;
using CourtApp.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{

    [Table("m_court_fee_structure", Schema = "ld")]
    public class CourtFeeStructureEntity : AuditableEntity
    {
        public Double MinValue { get; set; }
        public Double MaxValue { get; set; }
        public Double Rate { get; set; }
        public Double FixAmount { get; set; }        
        public int StateId { get; set; }
        public virtual StateEntity State { get; set; }
    }
}
