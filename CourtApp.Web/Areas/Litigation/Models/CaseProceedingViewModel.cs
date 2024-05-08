using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class CaseProceedingViewModel
    {
        #region Select List Area
        public SelectList Heads { get; set; }
        public SelectList SubHeads { get; set; }
        public SelectList NextStages { get; set; }
        #endregion
        public List<Guid> SelectedCases { get; set; }
        public List<Guid> SelectedSubHeads { get; set; }
        public Guid HeadId { get; set; }
        public Guid StageId { get; set; }
        public DateTime NextDate { get; set; }
        public string Remark { get; set; }
    }
}
