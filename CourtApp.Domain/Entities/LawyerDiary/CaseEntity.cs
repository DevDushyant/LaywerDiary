using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{

    [Table("u_case_detail", Schema = "ld")]
    public class CaseEntity : AuditableEntity
    {
        [Key]
       
        public new Guid Id { get; set; }

        [Required]
        public DateTime InstitutionDate { get; set; }

        public int LinkedClient { get; set; }
        [ForeignKey("LinkedClient")]
        public virtual ClientEntity Client { get; set; }

        public int CaseNatureId { get; set; }
        [ForeignKey("CaseNatureId")]
        public virtual CaseNatureEntity CaseNature { get; set; }

        public int TypeOfCaseId { get; set; }
        [ForeignKey("TypeOfCaseId")]
        public virtual TypeOfCasesEntity Typeofcases { get; set; }

        public int CourtTypeId { get; set; }
        [ForeignKey("CourtTypeId")]
        public virtual CourtTypeEntity CourtType { get; set; }

        public int CourtId { get; set; }
        [ForeignKey("CourtId")]
        public virtual CourtMasterEntity Court { get; set; }

        public int CaseTypeId { get; set; }
        [ForeignKey("CaseTypeId")]
        public CaseKindEntity CaseType { get; set; }

        [Required]
        public string CaseNumber { get; set; }

        [Required]
        public int CaseYear { get; set; }

        [Required]
        public string TitleFirst { get; set; }

        [Required]
        public int FirstTitleType { get; set; }

        [Required]
        public string TitleSecond { get; set; }

        [Required]
        public int SecondTitleType { get; set; }
        public DateTime? NextDate { get; set; }
        public string CaseStageCode { get; set; }
        public DateTime? CaseAgainstDecisionDate { get; set; }

        public int? AgainstCourtTypeId { get; set; }
        [ForeignKey("AgainstCourtTypeId")]
        public virtual CourtTypeEntity AgainstCourtType { get; set; }

        public int? AgainstCourtId { get; set; }
        [ForeignKey("AgainstCourtId")]
        public virtual CourtMasterEntity AgainstCourt { get; set; }
        public string AgainstCaseNumber { get; set; }
        public int? AgainstYear { get; set; }
        public Guid LinkedCaseId { get; set; }
    }
}
