using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class AssignCaseViewModel
    {
        public Guid Id { get; set; }
        public SelectList Lawyers { get; set; }
        public Guid CaseId { get; set; }
        public Guid LawyerId { get; set; }
    }
}
