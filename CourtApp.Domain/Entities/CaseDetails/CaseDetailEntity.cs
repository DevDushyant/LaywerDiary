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
        #region Common Properties Among all Court Type
        public  DateTime InstitutionDate { get; set; }
        public int StateId { get; set; }
        public Guid CourtTypeId { get; set; }

        /// <summary>
        /// This property is used commonly for High court bench. 
        /// And Other Court also, linked with CourtBenchEntity
        /// </summary>
        public Guid CourtBenchId { get; set; }
        public Guid CaseCategoryId { get; set; }
        public Guid CaseTypeId { get; set; }
        public string CaseNo { get; set; }
        public int CaseYear { get; set; }        
        public string FirstTitle { get; set; }
        public Guid FTitleId { get; set; }
        public string SecondTitle { get; set; }
        public Guid STitleId { get; set; }
        public string CisNumber { get; set; }
        public int CisYear { get; set; }
        public string CnrNumber { get; set; }
        public DateTime? NextDate { get; set; }
        public Guid? CaseStageId { get; set; }
        public Guid? LinkedCaseId { get; set; }
        public Guid? ClientId { get; set; }
        #endregion

        #region Other than High Court Propeties
        public Guid? CourtDistrictId { get; set; }
        public Guid? ComplexId { get; set; }

        #endregion

        #region HighCourt Properties        
        public int StrengthId { get; set; }
        #endregion

        #region Navigational Properies which shows the relationship
        public virtual StateEntity State { get; set; }
        public virtual CourtTypeEntity CourtType { get; set; }
        public virtual CourtDistrictEntity CourtDistrict { get; set; }
        public virtual FSTitleEntity STitle { get; set; }
        public virtual FSTitleEntity FTitle { get; set; }
        public virtual CourtBenchEntity CourtBench { get; set; }
        public virtual TypeOfCasesEntity CaseType { get; set; }
        public virtual NatureEntity CaseCategory { get; set; }
        public virtual CourtComplexEntity Complex { get; set; }
        public virtual CaseStageEntity CaseStage { get; set; }
        public ICollection<CaseDetailAgainstEntity> CaseAgainstEntities { get; set; } = new List<CaseDetailAgainstEntity>();
        #endregion
    }
}
