using AuditTrail.Abstrations;
using CourtApp.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_court", Schema = "ld")]    
    public class CourtMasterEntity : AuditableEntity
    {
        
        public new Guid Id { get; set; }
        public required string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Bench { get; set; }
        public string HeadQuerter { get; set; }
        public string Address { get; set; }

        [ForeignKey("StateCode")]
        public virtual StateEntity State { get; set; }

        [ForeignKey("DistrictCode")]
        public virtual DistrictEntity District { get; set; }

        [ForeignKey("CourtTypeId")]
        public virtual CourtTypeEntity CourtType { get; set; }
    }
}