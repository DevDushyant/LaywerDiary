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

        #endregion

        #region Upsert properties

        public  Guid Id { get; set; }
        public Guid LinkedCaseId { get; set; }
        public DateTime InstitutionDate { get; set; }
        public int LinkedClient { get; set; }
        public int CaseNatureId { get; set; }
        public int TypeOfCaseId { get; set; }
        public int CourtTypeId { get; set; }
        public int CourtId { get; set; }
        public int CaseTypeId { get; set; }
        public string CaseNumber { get; set; }
        public int CaseYear { get; set; }
        public string TitleFirst { get; set; }
        public int FirstTitleType { get; set; }
        public string TitleSecond { get; set; }
        public int SecondTitleType { get; set; }
        public DateTime NextDate { get; set; }
        public string CaseStageCode { get; set; }
        public DateTime CaseAgainstDecisionDate { get; set; }
        public int AgainstCourtTypeId { get; set; }
        public int AgainstCourtId { get; set; }
        public string AgainstCaseNumber { get; set; }
        public int AgainstYear { get; set; }

        #endregion

        
    }
}
