using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_court_type",Schema ="ld")]
    public class CourtTypeEntity : AuditableEntity
    {        
        [Required]
        public string CourtType { get; set; }
        public string Abbreviation { get; set; }
    }
}