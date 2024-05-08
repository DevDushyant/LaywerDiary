using CourtApp.Application.DTOs.CaseWorking;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class CaseWorkingViewModel
    {
        public Guid Id { get; set; }

        #region Select List Area
        public SelectList CaseTitles { get; set; }
        public SelectList Works { get; set; }
        public SelectList SubWorks { get; set; }
        #endregion
        
        public Guid WorkId { get; set; }
        public List<Guid> SubWorkId { get; set; }
        public DateTime WorkingDate { get; set; }
        public string Remark { get; set; }

        #region For Displaying Case Working
        public Guid CaseId { get; set; }
        public string CaseDetail { get; set; }
        public List<AssignedWork> AWorks { get; set; }
        #endregion
    }    
}
