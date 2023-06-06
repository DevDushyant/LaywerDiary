using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Models
{
    public class TypeOfCasesViewModel

    {
        public int Id { get; set; }
        public int CaseNatureId { get; set; }
        public SelectList CaseNatures { get; set; }
        public string CaseNature { get; set; }
        public string Typeofcases { get; set; }
    }
}
