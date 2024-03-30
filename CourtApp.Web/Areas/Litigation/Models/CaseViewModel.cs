using CourtApp.Domain.Entities.LawyerDiary;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.Litigation.Models
{
    public class CaseViewModel
    {

        #region Select List Region
        public SelectList CaseNatures { get; set; }
        public SelectList TypeOfCases { get; set; }
        public SelectList CourtTypes { get; set; }
        public SelectList Courts { get; set; }
        public SelectList CaseTypes { get; set; }
        public SelectList Year { get; set; }        
        public SelectList CaseStages { get; set; }
        public SelectList ClientList { get; set; }
        public SelectList CaseStatusList { get; set; }
        public SelectList FirstTitleList { get; set; }
        public SelectList SecondTitleList { get; set; }
        public SelectList LinkedBy { get; set; }
        public SelectList Cadres { get; set; }

        #endregion

        #region Upsert properties
        public  Guid Id { get; set; }        
        public DateTime InstitutionDate { get; set; }
        public Guid ClientId { get; set; }
        public Guid NatureId { get; set; }
        public Guid TypeCaseId { get; set; }
        public Guid CourtTypeId { get; set; }
        public Guid CourtId { get; set; }
        public Guid CaseTypeId { get; set; }
        public string Number { get; set; }
        public int CaseYear { get; set; }
        public string CisNumber { get; set; }
        public int CisYear { get; set; }
        public string CnrNumber { get; set; }
        public string FirstTitle { get; set; }
        public int TitleTypeFirst { get; set; }
        public string SecondTitle { get; set; }
        public int TitleTypeSecond { get; set; }
        public DateTime NextDate { get; set; }
        public string CaseStageCode { get; set; }
        public Guid LinkedWith { get; set; }
        public List<CaseAgainstViewModel> AgainstCaseDetails { get; set; }

        #endregion   
    }    
}
