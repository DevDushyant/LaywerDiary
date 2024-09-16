using System.Collections.Generic;
namespace CourtApp.Application.DTOs.FormBuilder
{
    public class CaseMappingDetailInfoDto
    {
        public string CaseTitle { get; set; }
        public string CaseNoYear { get; set; }
        public string CaseType { get; set; }
        public string TemplateName { get; set; }
        public string TemplatePath { get; set; }
        public List<CaseCompleteTitle> CaseCompleteTitles { get; set; }
        public List<MappingDetails> TagValues { get; set; }
    }
    public class CaseCompleteTitle
    {
        public string Title { get; set; }
    }
    public class MappingDetails
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
