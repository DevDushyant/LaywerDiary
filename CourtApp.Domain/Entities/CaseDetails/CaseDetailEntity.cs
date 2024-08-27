using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.CaseDetails
{
    [Table("case_detail", Schema = "ld")]
    public class CaseDetailEntity : AuditableEntity
    {
        #region Mandatory Properties
        public new Guid Id { get; set; }
        public required DateTime InstitutionDate { get; set; }
        public int StateId { get; set; }
        public virtual StateEntity State { get; set; }
        public required Guid CourtTypeId { get; set; }
        public virtual CourtTypeEntity CourtType { get; set; }
        public required Guid CaseCategoryId { get; set; }
        public virtual NatureEntity CaseCategory { get; set; }
        public required Guid CaseTypeId { get; set; }
        public virtual TypeOfCasesEntity CaseType { get; set; }
        public required Guid CourtBenchId { get; set; }
        public virtual CourtBenchEntity CourtBench { get; set; }
        public string CaseNo { get; set; }
        public required int CaseYear { get; set; }
        public required string FirstTitle { get; set; }
        public required Guid FTitleId { get; set; }
        public virtual FSTitleEntity FTitle { get; set; }
        public required string SecondTitle { get; set; }
        public required Guid STitleId { get; set; }
        public virtual FSTitleEntity STitle { get; set; }

        #endregion
        public string CisNumber { get; set; }
        public int CisYear { get; set; }
        public string CnrNumber { get; set; }
        public DateTime? NextDate { get; set; }
        public Guid? CaseStageId { get; set; }
        public virtual CaseStageEntity CaseStage { get; set; }
        public Guid? LinkedCaseId { get; set; }
        public Guid? ClientId { get; set; }
        public ICollection<CaseDetailAgainstEntity> CaseAgainstEntities { get; set; } = new List<CaseDetailAgainstEntity>();

    }
}
