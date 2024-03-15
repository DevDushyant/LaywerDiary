using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_nature", Schema = "ld")]
    public class NatureEntity : AuditableEntity
    {       
        public new Guid Id { get; set; }
        public required string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
}