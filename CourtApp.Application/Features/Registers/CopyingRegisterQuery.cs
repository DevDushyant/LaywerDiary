using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.DTOs.Registers;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using CourtApp.Application.Extensions;
using CourtApp.Domain.Entities.LawyerDiary;
using System.Collections.Generic;
namespace CourtApp.Application.Features.Registers
{
    public class CopyingRegisterQuery : IRequest<PaginatedResult<CopyDisposalResponse>>
    {
        public DateTime FromDt { get; set; }
        public DateTime ToDt { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int SearchType { get; set; }
    }
    public class CopyingRegisterQueryHandler : IRequestHandler<CopyingRegisterQuery, PaginatedResult<CopyDisposalResponse>>
    {
        private readonly IUserCaseRepository _caseRepo;
        private readonly ICaseWorkRepository _wRepo;
        public CopyingRegisterQueryHandler(IUserCaseRepository _caseRepo
            , ICaseWorkRepository _wRepo)
        {
            this._caseRepo = _caseRepo;
            this._wRepo = _wRepo;
        }
        public async Task<PaginatedResult<CopyDisposalResponse>> Handle(CopyingRegisterQuery request, CancellationToken cancellationToken)
        {
            int[] status = new int[] { 1, 2 };
            IQueryable<CaseWorkEntity> cWdt = _wRepo.Entities.Where(w => w.WorkType.Abbreviation == "COPY"
            && status.Contains(w.Status)).Distinct();
            if (request.SearchType == 2) cWdt = cWdt.Where(s => s.Status == 1);
            if (request.SearchType == 3) cWdt = cWdt.Where(s => s.Status == 2);            
            if (cWdt.Count() > 0)
            {
                var fndt = from cd in _caseRepo.Entites
                           join w in cWdt on cd.Id equals w.CaseId
                           select new CopyDisposalResponse
                           {
                               Id = cd.Id,
                               CourtType = cd.CourtType.CourtType,
                               CaseNo = cd.CaseNo,
                               CaseYear = cd.CaseYear,
                               CourtBench = cd.CourtBench.CourtBench_En,
                               FirstTitle = cd.FirstTitle,
                               SecondTitle = cd.SecondTitle,
                               CaseType = cd.CaseType.Name_En,
                               CaseAbbretion = cd.CaseType.Abbreviation,
                               Reason = w.Work.Name_En,
                               AppliedOn = w.AppliedOn != null ? w.AppliedOn.Value.ToString("dd/MM/yyyy") : "",
                               ReceivedOn = w.ReceivedOn != null ? w.ReceivedOn.Value.ToString("dd/MM/yyyy") : "",
                           };
                if (fndt != null)
                    return await fndt.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            }
            return null;
        }
    }
}
