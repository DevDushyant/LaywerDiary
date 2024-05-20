using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.DTOs.CaseWorking;
using CourtApp.Application.Features.BookMasters.Query;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
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
        private readonly IWorkMasterSubRepository _WorkMasterSub;
        private readonly ICaseWorkRepository _CaseWorkRepo;
        private readonly IMapper _mapper;
        public GetCaseHistoryQueryHandler(IUserCaseRepository _CaseRepo,           
            IWorkMasterSubRepository _WorkMasterSub, IMapper _mapper,
            ICaseWorkRepository _CaseWorkRepo)
        {
            this._CaseRepo = _CaseRepo;           
            this._WorkMasterSub = _WorkMasterSub;
            this._mapper = _mapper;
            this._CaseWorkRepo = _CaseWorkRepo;
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
                var cwd = await _CaseWorkRepo.GetListAsync();
                var cwdata = cwd.Where(w => w.CaseId == request.CaseId)
                    .Select(s => new CaseHistoryData
                    {
                        Date = s.WorkingDate.ToString("dd/MM/yyyy"),
                        Stage = "",
                        Activity = s.Work.Name_En
                    });
                chr.History = cwdata.ToList();
                return Result<CaseHistoryResposnse>.Success(chr);
            }
            return null;
        }
    }
}
