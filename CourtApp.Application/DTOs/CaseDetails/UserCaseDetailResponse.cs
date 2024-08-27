using System;

namespace CourtApp.Application.DTOs.CaseDetails
{
    public class UserCaseDetailResponse
    {
        public Guid Id { get; set; }
        public string InstitutionDate { get; set; }
        public int StateId { get; set; }
        public Guid CourtBenchId { get; set; }
        public string CourtBench { get; set; }
        public Guid CourtTypeId { get; set; }
        public string CourtType { get; set; }
        public Guid CaseTypeId { get; set; }
        public string CaseType { get; set; }
        public Guid CaseCategoryId { get; set; }
        public string CaseCategory { get; set; }
        public string CaseNo { get; set; }
        public int CaseYear { get; set; }
        public int CisNo { get; set; }
        public int CisYear { get; set; }
        public string CnrNumber { get; set; }
        public string FirstTitle { get; set; }
        public string FirstTitleCode { get; set; }
        public string SecondTitle { get; set; }
        public string SecondTitleCode { get; set; }
        public string NextDate { get; set; }
        public string CaseStageCode { get; set; }
        public string CaseStage { get; set; }
        public string Appearence { get; set; }
        public Guid LinkedCaseId { get; set; }
        public Guid LinkedClientId { get; set; }

    }
}
