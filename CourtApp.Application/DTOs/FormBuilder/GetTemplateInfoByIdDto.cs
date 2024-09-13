using System.Collections.Generic;

namespace CourtApp.Application.DTOs.FormBuilder
{
    public class GetTemplateInfoByIdDto
    {
        public string TemplateName { get; set; }
        public string TemplatePath { get; set; }
        public List<Tags> Tags { get; set; }
    }
    public class Tags
    {
        public string Tag { get; set; }
    }
}
