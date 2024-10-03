using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.CaseDetails
{

    [Table("case_detail_against", Schema = "ld")]
    public class CaseDetailAgainstEntity
    {
        public Guid Id { get; set; }
        public DateTime ImpugedOrderDate { get; set; }
        public Guid CourtTypeId { get; set; }
        public Guid CourtBenchId { get; set; }
        public int StateId { get; set; }
        public int StrengthId { get; set; }
        public string CaseNo { get; set; }
        public int CaseYear { get; set; }
        public string CisNo { get; set; }
        public int CisYear { get; set; }
        public string CnrNo { get; set; }
        public string OfficerName { get; set; }
        public string Cadre { get; set; }
        public virtual StateEntity State { get; set; }        
        public virtual CourtTypeEntity CourtType { get; set; }               
        public virtual CourtBenchEntity CourtBench { get; set; }
        public virtual CaseDetailEntity CaseId { get; set; }
        public Guid CaseCategoryId { get; set; }
        public virtual NatureEntity CaseCategory { get; set; }
        public Guid CaseTypeId { get; set; }
        public virtual TypeOfCasesEntity CaseType { get; set; }
        public Guid CourtComplexId { get; set; }
        public virtual CourtComplexEntity CourtComplex { get; set; }

        public required Guid CourtDistrictId { get; set; }
        public virtual CourtDistrictEntity CourtDistrict { get; set; }
    }
}
