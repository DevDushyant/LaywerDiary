using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{

    [Table("client_case", Schema = "ld")]
    public class CaseEntity : AuditableEntity
    {
        
        public new Guid Id { get; set; }
        public required DateTime InstitutionDate { get; set; }
        public required string Number { get; set; }
        public required int Year { get; set; }
        public required int TitleTypeFirst { get; set; }
        public required string FirstTitle { get; set; }
        public required int TitleTypeSecond { get; set; }
        public required string SecondTitle { get; set; }
        public string CaseStageCode { get; set; }
        public DateTime? CaseAgainstDecisionDate { get; set; }
        public string AgainstCaseNumber { get; set; }
        public int? AgainstYear { get; set; }
        public Guid LinkedCaseId { get; set; }
        //public DateTime NextDate { get; set; }

        #region Relation Area
        [ForeignKey("ClientId")]
        public virtual ClientEntity Client { get; set; }

        [ForeignKey("NatureId")]
        public virtual NatureEntity CaseNature { get; set; }

        [ForeignKey("TypeCaseId")]
        public virtual TypeOfCasesEntity TypeOfCase { get; set; }

        [ForeignKey("CourtTypeId")]
        public virtual CourtTypeEntity CourtType { get; set; }

        [ForeignKey("CourtId")]
        public virtual CourtMasterEntity Court { get; set; }
        [ForeignKey("CaseTypeId")]
        public CaseKindEntity CaseType { get; set; }
        [ForeignKey("AgainstCourtTypeId")]
        public virtual CourtTypeEntity AgainstCourtType { get; set; }
        [ForeignKey("AgainstCourtId")]
        public virtual CourtMasterEntity AgainstCourt { get; set; }
        #endregion
    }
}
