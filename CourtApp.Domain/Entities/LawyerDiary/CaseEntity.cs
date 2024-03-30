using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{

    [Table("case_detail", Schema = "ld")]
    public class CaseEntity : AuditableEntity
    {
        public new Guid Id { get; set; }
        public required DateTime InstitutionDate { get; set; }

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
        public required string Number { get; set; }
        public required int Year { get; set; }
        public int CisNumber { get;set; }
        public int CisYear { get;set; }
        public int CnrNumber { get;set; }
        public required string FirstTitle { get; set; }
        public required int TitleTypeFirst { get; set; }
        public required string SecondTitle { get; set; }
        public required int TitleTypeSecond { get; set; }
        public DateTime NextDate { get; set; }
        public string CaseStageCode { get; set; }
        public Guid LinkedCaseId { get; set; }
        public ICollection<AgainstCaseDetails> AgainstCaseDetails { get; set; }

        [ForeignKey("ClientId")]
        public virtual ClientEntity Client { get; set; }
    }
}
