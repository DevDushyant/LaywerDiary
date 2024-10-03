using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.DTOs.CaseDetails
{
    public class CaseDetailInfoDto
    {
        public Guid Id { get; set; }
        public string InstitutionDate { get; set; }
        public string State { get; set; }
        public bool IsHighCourt { get; set; }
        public string CourtType { get; set; }
        public string CourtBench { get; set; }
        public string DistrictCourt { get; set; }
        public string CourtComplex { get; set; }
        public string CaseNo { get; set; }
        public string CaseYear { get; set; }
        public string CaseCategory { get; set; }        
        public string CaseType { get; set; }        
        public string CisNo { get; set; }
        public string CisYear { get; set; }
        public string CnrNo { get; set; }       
        public string FirstTitle { get; set; }       
        public string FirstTitleDetail { get; set; }        
        public string SecondTitle { get; set; }        
        public string SecondTitleDetail { get; set; }        
        public string NextDate { get; set; }        
        public string CaseStage { get; set; }
        public bool IsCaseAgainstDecision { get; set; }
        public List<AgainstCaseDetail> AgainstCases { get; set; }
        //public ClientDetailViewModel ClientDetail { get; set; }
    }
}
