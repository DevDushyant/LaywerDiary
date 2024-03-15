using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace CourtApp.Web.Areas.LawyerDiary.Models
{
    public class ProceedingSubHeadViewModel
    {
        public Guid Id { get; set; }
        public Guid PHeadId { get; set; }
        public string PheadName { get; set; }
        public SelectList PHeads { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
}
