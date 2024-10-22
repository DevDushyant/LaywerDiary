using CourtApp.Application.Features.Case;
using System;
using System.Collections.Generic;

namespace CourtApp.Application.DTOs.CaseDetails
{
    public class UserCaseDetailResponse
    {
        public Guid Id { get; set; }
        public DateTime InstitutionDate { get; set; }
        public Guid CourtTypeId { get; set; }
        public Guid CaseCategoryId { get; set; }
        public Guid CaseTypeId { get; set; }
        public Guid CourtBenchId { get; set; }
        public string CaseNo { get; set; }
        public int CaseYear { get; set; }
        public string FirstTitle { get; set; }
        public Guid FirstTitleCode { get; set; }
        public string SecondTitle { get; set; }
        public Guid SecoundTitleCode { get; set; }
        public string CisNumber { get; set; }
        public int? CisYear { get; set; }
        public string CnrNumber { get; set; }
        public DateTime? NextDate { get; set; }
        public Guid CaseStageCode { get; set; }
        public Guid? LinkedCaseId { get; set; }
        public Guid? CourtDistrictId { get; set; }
        public Guid? ComplexBenchId { get; set; }
        public List<CaseAgainstEntityModel> AgainstCaseDetails { get; set; }
        public bool IsHighCourt { get; set; }
        public int? StateId { get; set; }
        public int? StrengthId { get; set; }
        public Guid? BenchId { get; set; }
        public Guid? CourtId { get; set; }
        public Guid CourtComplexId { get; set; }

    }
}
