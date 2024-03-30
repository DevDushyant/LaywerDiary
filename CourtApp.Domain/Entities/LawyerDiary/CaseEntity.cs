using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
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

        public Guid NatureId { get; set; }
        public Guid TypeCaseId { get; set; }
        public Guid CourtTypeId { get; set; }
        public Guid CourtId { get; set; }
        public Guid CaseTypeId { get; set; }
        public List<AgainstCaseDetails> AgainstCaseDetails { get; set; }
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
        #endregion
    }
}
