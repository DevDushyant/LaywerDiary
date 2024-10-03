﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class FmpViewModel
    {
        public SelectList FormTypes { get; set; }
        public string FormTypeId { get; set; }
        public SelectList Cases { get; set; }
        public List<Guid> CaseIds { get; set; }
        public SelectList Titles { get; set; }
        public List<Guid> TitleIds { get; set; }
        //public SelectList InvolvedParties { get; set; }
    }
}
