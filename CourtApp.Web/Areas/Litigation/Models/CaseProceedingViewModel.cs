using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class CaseProceedingViewModel
    {
        #region Select List Area
        public SelectList ProceedingTypes { get; set; }
        public SelectList Proceedings { get; set; }
        public SelectList Stages { get; set; }
        #endregion
        public Guid CaseId { get; set; }
        public Guid ProceedingTypeId { get; set; }
        public List<Guid> ProceedingsIds { get; set; }        
        public Guid StageId { get; set; }
        public DateTime NextDate { get; set; }
        public string Remark { get; set; }
    }
}
