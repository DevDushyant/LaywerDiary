using System;

namespace CourtApp.Web.Areas.Report.Models
{
    public class DisposalRegisterViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Reason { get; set; }
        public string DisposalDate { get; set; }
    }
}
