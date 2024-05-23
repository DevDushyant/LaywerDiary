using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.DTOs.CaseWorking;
using CourtApp.Application.Features.BookMasters.Query;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails
{
    public class GetCaseHistoryQuery : IRequest<Result<CaseHistoryResposnse>>
    {
        public Guid CaseId { get; set; }
    }
    public class GetCaseHistoryQueryHandler : IRequestHandler<GetCaseHistoryQuery, Result<CaseHistoryResposnse>>
    {
        private readonly IUserCaseRepository _CaseRepo;
        private readonly ICaseWorkRepository _CaseWorkRepo;
        private readonly ICaseProceedingRepository _ProceedingRepo;
        private readonly IMapper _mapper;
        public GetCaseHistoryQueryHandler(IUserCaseRepository _CaseRepo,
            IMapper _mapper,
            ICaseWorkRepository _CaseWorkRepo,
            ICaseProceedingRepository _ProceedingRepo)
        {
            this._CaseRepo = _CaseRepo;
            this._mapper = _mapper;
            this._CaseWorkRepo = _CaseWorkRepo;
            this._ProceedingRepo = _ProceedingRepo;
        }
        public async Task<Result<CaseHistoryResposnse>> Handle(GetCaseHistoryQuery request, CancellationToken cancellationToken)
        {
            var detail = await _CaseRepo.GetByIdAsync(request.CaseId);
            if (detail != null)
            {
                var mappeddata = _mapper.Map<UserCaseDetailResponse>(detail);
                CaseHistoryResposnse chr = new CaseHistoryResposnse();
                chr.Id = request.CaseId;
                chr.CaseNoYear = detail.CaseNo + "/" + detail.CaseYear;
                chr.Title = detail.FirstTitle + " Vs " + detail.SecondTitle;
                chr.CourtType = detail.CourtType.CourtType;
                chr.Court = detail.CourtBench.CourtBench_En;
                var caseWorks = await _CaseWorkRepo.GetWorkByCaseIdAsync(request.CaseId);
                var cwdata = caseWorks
                    .Select(s => new CaseHistoryData
                    {
                        Date = s.WorkingDate.ToString("dd/MM/yyyy"),
                        Stage = "",
                        Activity = s.Work.Name_En,
                        Type = "Work"
                    });

                var cprocs = await _ProceedingRepo.GetProceedingByCaseIdAsync(request.CaseId);
                var cprocdt = cprocs.Where(w => w.CaseId == request.CaseId)
                    .Select(s => new CaseHistoryData
                    {
                        Date = s.NextDate.ToString("dd/MM/yyyy"),
                        Stage = s.Stage.CaseStage,
                        Activity = s.SubHead.Name_En,
                        Type = "Proceeding"
                    });

                var historydt = cwdata.Concat(cprocdt);
                chr.History = historydt.ToList();
                return Result<CaseHistoryResposnse>.Success(chr);
            }
            return null;
        }
    }
}
