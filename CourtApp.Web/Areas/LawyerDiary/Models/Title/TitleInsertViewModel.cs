using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CourtApp.Web.Areas.LawyerDiary.Models.Title
{
    public class TitleInsertViewModel
    {
        public SelectList CaseDataList { get; set; }
        public SelectList TypeDataList { get; set; }
        public List<string> Title { get; set; }
    }
}
