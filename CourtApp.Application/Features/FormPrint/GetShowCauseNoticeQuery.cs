using AspNetCoreHero.Results;
using CourtApp.Application.DTOs.FormPrint;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.FormPrint
{
    public class GetShowCauseNoticeQuery : IRequest<Result<List<ShowCauseNoticeResponse>>>
    {
        public List<Guid> CaseIds { get; set; }
    }
    public class GetShowCauseNoticeQueryHandler : IRequestHandler<GetShowCauseNoticeQuery, Result<List<ShowCauseNoticeResponse>>>
    {
        private readonly IUserCaseRepository _CaseRepo;
        private readonly ICaseTitleRepository _TitleRepo;
        public GetShowCauseNoticeQueryHandler(IUserCaseRepository _CaseRepo, ICaseTitleRepository _TitleRepo)
        {
            this._CaseRepo = _CaseRepo;
            this._TitleRepo = _TitleRepo;
        }

        public async Task<Result<List<ShowCauseNoticeResponse>>> Handle(GetShowCauseNoticeQuery request, CancellationToken cancellationToken)
        {
            var CaseTypeAbb = new List<string>();
            CaseTypeAbb.Add("Civil");
            CaseTypeAbb.Add("Writ");
            var Results = from c in _CaseRepo.Entites.Include(ct => ct.CaseType)
                          .Where(w => request.CaseIds.Contains(w.Id) && CaseTypeAbb.Contains(w.CaseType.Abbreviation))
                          join t in _TitleRepo.Titles on c.Id equals t.CaseId
                          select new ShowCauseNoticeResponse
                          {
                             Petitioner = c.FirstTitle,
                             Respondent = c.SecondTitle,
                             FirstTitle = t.TypeId == 1 ? t.Title : "",
                             SecondTitle = t.TypeId == 2 ? t.Title : "",
                             CaseNoYear = c.CaseNo + "/" + c.CaseYear,
                             CaseType = c.CaseType.Name_En,                             
                          };
            return Result<List<ShowCauseNoticeResponse>>.Success(Results.ToList());
        }
    }
}
