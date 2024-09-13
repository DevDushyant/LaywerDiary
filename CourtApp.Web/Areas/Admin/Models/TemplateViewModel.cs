using System;

namespace CourtApp.Web.Areas.Admin.Models
{
    public class TemplateViewModel
    {
        public Guid Id { get; set; }
        public string TemplateName { get; set; }
        public string TemplateBody { get; set; }
    }
}
