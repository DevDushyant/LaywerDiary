using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class FormBuilderViewModel
    {
        public Guid Id { get; set; }
        public Guid TemplateId { get; set; }
        public SelectList Templates { get; set; }
        public Guid CaseId { get; set; }
        public SelectList Cases { get; set; }
        public List<FormProperties> FieldDetails { get; set; }
    }    
}
