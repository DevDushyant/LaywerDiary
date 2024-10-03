using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class CaseDetailInfoViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Institution Date")]
        public string InstitutionDate { get; set; }
        public string State { get; set; }
        public bool IsHighCourt { get; set; }=false;
        [DisplayName("Court Type")]
        public string CourtType { get; set; }

        [DisplayName("Court/Bench")]
        public string CourtBench { get; set; }

        [DisplayName("District Court")]
        public string DistrictCourt { get; set; }

        [DisplayName("Court Complex")]
        public string CourtComplex { get; set; }

        [DisplayName("Case No/Year")]
        public string CaseNoYear { get; set; }
        public string CaseNo { get; set; }
        public string CaseYear { get; set; }

        [DisplayName("Case Category")]
        public string CaseCategory { get; set; }
        [DisplayName("Case Type")]
        public string CaseType { get; set; }
        [DisplayName("Cis No/Year")]
        public string CisNoYear { get; set; }

        public string CisNo { get; set; }
        public string CisYear { get; set; }

        [DisplayName("Cnr No")]
        public string CnrNo { get; set; }
        [DisplayName("First Title Type")]
        public string FirstTitle { get; set; }

        [DisplayName("First Title")]
        public string FirstTitleDetail { get; set; }

        [DisplayName("Secound Title Type")]
        public string SecondTitle { get; set; }

        [DisplayName("Secound Title")]
        public string SecondTitleDetail { get; set; }

        [DisplayName("Next Date")]
        public string NextDate { get; set; }

        [DisplayName("Case Stage")]
        public string CaseStage { get; set; }
        public bool IsCaseAgainstDecision { get; set; }
        public List<AgainstCaseDecisionViewModel> AgainstCases { get; set; }
        //public ClientDetailViewModel ClientDetail { get; set; }
    }
}
