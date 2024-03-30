using System;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class TitleGetViewModel
    {
        public Guid CaseId { get; set; }
        public string Type { get; set; }
        public string CaseDetail { get; set; }
    }
}
