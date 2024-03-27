using CourtApp.Domain.Entities.LawyerDiary;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace CourtApp.Web.Areas.LawyerDiary.Models
{
    public class WorkMasterSubViewModel
    {
        public Guid Id { get; set; }
        public Guid WMasterId { get; set; }
        public SelectList WMasters { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public virtual WorkMasterEntity WorkMaster { get; set; } 
    }
}
