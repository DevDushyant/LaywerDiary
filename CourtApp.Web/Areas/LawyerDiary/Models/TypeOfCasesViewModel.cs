using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.LawyerDiary.Models
{
    public class TypeOfCasesViewModel

    {
        public Guid Id { get; set; }        
        public SelectList CaseNatures { get; set; }
        public string CaseNature { get; set; }
        public Guid NatureId { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
}
