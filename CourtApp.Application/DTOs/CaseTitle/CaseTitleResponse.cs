using System;
using System.Collections.Generic;

namespace CourtApp.Application.DTOs.CaseTitle
{
    public class CaseTitleResponse
    {
        public Guid Id { get; set; }        
        public string CaseDetail { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public List<ApplicantDetailDto> CaseApplicantDetails { get; set; }
    }
    
}
