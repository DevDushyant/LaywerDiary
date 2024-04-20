using CourtApp.Domain.Entities.LawyerDiary;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace CourtApp.Web.Areas.LawyerDiary.Models
{
    public class ProceedingSubHeadViewModel
    {
        public Guid Id { get; set; }
        public string Head { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public Guid HeadId { get; set; }
        public string PheadName { get; set; }
        public SelectList PHeads { get; set; }      
        
    }
}
