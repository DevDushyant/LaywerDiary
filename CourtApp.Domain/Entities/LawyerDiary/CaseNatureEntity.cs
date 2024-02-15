//using AspNetCoreHero.Abstractions.Domain;
using AuditTrail.Abstrations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_casenature", Schema = "ld")]
    public class CaseNatureEntity : AuditableEntity
    {
        [Required]
        public string  CaseNature { get; set; }
    }
}