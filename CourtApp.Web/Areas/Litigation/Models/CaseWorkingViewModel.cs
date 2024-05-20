using CourtApp.Application.DTOs.CaseWorking;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class CaseWorkingViewModel
    {
        public Guid Id { get; set; }
        public SelectList WorkTypes { get; set; }
        public SelectList Works { get; set; }
        public Guid WorkTypeId { get; set; }
        public List<Guid> WorkId { get; set; }
        public DateTime WorkingDate { get; set; }
        public string Remark { get; set; }
        //public List<Guid> SelectedCases { get; set; }

        #region For Displaying Case Working
        public Guid CaseId { get; set; }
        public string CaseDetail { get; set; }
        public List<AssignedWork> AWorks { get; set; }
        #endregion
    }    
}
