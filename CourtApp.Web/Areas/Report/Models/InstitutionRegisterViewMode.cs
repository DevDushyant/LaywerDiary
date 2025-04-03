using System.Collections.Generic;

namespace CourtApp.Web.Areas.Report.Models
{
    public class InstitutionRegisterViewMode
    {
        public List<InstituteModel> dtmodel { get; set; }
    }
    public class InstituteModel : CaseDetailViewModel
    {
        public string Reference { get; set; }
    }
}
