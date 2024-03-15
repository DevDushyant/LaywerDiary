using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{

    [Table("m_court_fee_type", Schema = "ld")]
    public class CourtFeeTypeEntity : AuditableEntity
    {
        public new Guid Id { get; set; }
        public required string CourtFeeType { get; set; }
    }
}
