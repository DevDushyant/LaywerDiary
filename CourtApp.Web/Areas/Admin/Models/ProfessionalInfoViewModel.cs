﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourtApp.Web.Areas.Admin.Models
{
    public class ProfessionalInfoViewModel
    {
        public string EnrollmentNo { get; set; }
        public string PracticeSince { get; set; }
        public SelectList Years { get; set; }
    }
}
