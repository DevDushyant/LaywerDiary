using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Commands.Cases
{
    public class CommandUpdateCaseEntry : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
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
    }
}
