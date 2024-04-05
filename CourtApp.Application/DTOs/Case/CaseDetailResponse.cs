using System;

namespace CourtApp.Application.DTOs.Case
{
    public class CaseDetailResponse
    {
        public Guid Id { get; set; }
        public string CaseTypeName { get; set; }
        public string CaseKindName { get; set; }
        public string CaseNumber { get; set; }
        public string NextHearingDate { get; set; }
        public string CaseStage { get; set; }        
        public string FirstTitle { get; set; }
        public string SecondTitle { get; set; }
        public string CourtName { get; set; }
    }
}
