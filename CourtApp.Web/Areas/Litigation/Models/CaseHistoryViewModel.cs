using System;
using System.Collections.Generic;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class CaseHistoryViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Court { get; set; }
        public string CaseNoYear { get; set; }
        public List<HistoryDetail> History { get; set; }
    }
    public class HistoryDetail
    {
        public string Type { get; set; }
        public string Date { get; set; }
        public string Stage { get; set; }
        public string Activity { get; set; }
    }
}
