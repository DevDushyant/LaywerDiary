using AuditTrail.Abstrations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_type_case", Schema = "ld")]
    public class TypeOfCasesEntity : AuditableEntity
    {
        [Required]
        [ForeignKey("CaseNature")]
        public int CaseNatureId { get; set; }
        public virtual CaseNatureEntity CaseNature { get; set; }

        [Required]
        public string Typeofcases { get; set; }
    }
}