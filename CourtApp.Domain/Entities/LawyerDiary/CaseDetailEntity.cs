using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("case_detail", Schema = "ld")]
    public class CaseDetailEntity : AuditableEntity
    {
        #region Mandatory Properties
        public new Guid Id { get; set; }
        public required DateTime InstitutionDate { get; set; }
        public required Guid CourtTypeId { get; set; }
        public virtual CourtTypeEntity CourtType { get; set; }
        public required Guid CaseCategoryId { get; set; }
        public virtual NatureEntity CaseCategory { get; set; }
        public required Guid CaseTypeId { get; set; }
        public virtual TypeOfCasesEntity CaseType { get; set; }
        public required Guid CourtBenchId { get; set; }
        public virtual CourtBenchEntity CourtBench { get; set; }
        public required string CaseNo { get; set; }
        public required int CaseYear { get; set; }
        public required string FirstTitle { get; set; }
        public required int FirstTitleCode { get; set; }
        public required string SecondTitle { get; set; }
        public required int SecoundTitleCode { get; set; }

        #endregion
        public string CisNumber { get; set; }
        public int CisYear { get; set; }
        public string CnrNumber { get; set; }
        public DateTime NextDate { get; set; }
        public string CaseStageCode { get; set; }
        public Guid LinkedCaseId { get; set; }
        public Guid ClientId { get; set; }
        public ICollection<CaseDetailAgainstEntity> CaseAgainstEntities { get; set; }
    }
}
