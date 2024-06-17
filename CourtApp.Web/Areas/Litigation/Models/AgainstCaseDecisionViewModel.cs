namespace CourtApp.Web.Areas.Litigation.Models
{
    public class AgainstCaseDecisionViewModel
    {
        public string ImpugedOrder { get; set; }
        public string InstitutionDate { get; set; }
        public string State { get; set; }
        public bool IsHighCourt { get; set; }
        public string CourtType { get; set; }
        public string CourtBench { get; set; }
        public string DistrictCourt { get; set; }
        public string CourtComplex { get; set; }
        public string CaseNoYear { get; set; }
        public string CaseCategory { get; set; }
        public string CaseType { get; set; }
        public string CisNoYear { get; set; }
        public string CnrNo { get; set; }
        public string OfficerName { get; set; }
        public string Cadre { get; set; }
    }
}
