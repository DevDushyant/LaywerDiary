using System;
using System.Collections.Generic;

namespace CourtApp.Application.DTOs.CaseDetails
{
    public class CaseHistoryResposnse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string CourtType { get; set; }
        public string Court { get; set; }
        public string CaseNoYear { get; set; }
        public List<CaseHistoryData> History { get; set; }
    }

    public class CaseHistoryData
    {
        public string Date { get; set; }
        public string Stage { get; set; }
        public string Activity { get; set; }
    }
}
