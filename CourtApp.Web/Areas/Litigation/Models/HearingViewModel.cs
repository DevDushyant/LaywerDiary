using System;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class HearingViewModel
    {
        public Guid Id { get; set; }
        public string CaseTitle { get; set; }        
        public bool Selected { get; set; }

    }
}
