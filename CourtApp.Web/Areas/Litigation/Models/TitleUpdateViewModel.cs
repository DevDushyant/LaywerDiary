using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class TitleUpdateViewModel
    {
        public Guid CaseId { get; set; }
        public SelectList CaseDataList { get; set; }
        public SelectList TypeDataList { get; set; }
        public List<string> Title { get; set; }
    }
}
