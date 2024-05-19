using AspNetCoreHero.Results;
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
        private readonly ICourtTypeCacheRepository _RepoCourtType;
        private readonly ICaseStageCacheRepository _RepoStage;
        private readonly ICourtBenchRepository _CourtRepo;
        private readonly ICaseWorkRepository _CaseWork;
        private readonly IWorkMasterSubRepository _WorkMasterSub;

        public GetCaseHistoryQueryHandler(IUserCaseRepository _CaseRepo, ICourtTypeCacheRepository _RepoCourtType,
            ICaseStageCacheRepository _RepoStage, ICourtBenchRepository courtRepo,
            ICaseWorkRepository _CaseWork, IWorkMasterSubRepository _WorkMasterSub)
        {
            this._CaseRepo = _CaseRepo;
            this._RepoCourtType = _RepoCourtType;
            this._RepoStage = _RepoStage;
            _CourtRepo = courtRepo;
            this._CaseWork = _CaseWork;
            this._WorkMasterSub = _WorkMasterSub;
        }
        public async Task<Result<CaseHistoryResposnse>> Handle(GetCaseHistoryQuery request, CancellationToken cancellationToken)
        {
            var CaseDetail = await _CaseRepo.GetByIdAsync(request.CaseId);
            var courtDetail = await _RepoCourtType.GetByIdAsync(CaseDetail.CourtTypeId);
            var court = await _CourtRepo.GetByIdAsync(CaseDetail.CourtBenchId);
            var CaseWork = _CaseWork.Entities.AsEnumerable()
                .Where(c => c.CaseId == request.CaseId)
                .Select(s => new CaseWorkDto
                {
                    WDate = s.WorkingDate.ToString("dd/MM/yyyy"),
                    WorkId = s.WorkId
                });

            var workdt = from w in CaseWork.ToList()
                         join sw in await _WorkMasterSub.GetListAsync() on w.WorkId equals sw.WorkId
                         select new CaseHistoryData
                         {
                             Date=w.WDate,
                             Stage="",
                             Activity=sw.Name_En
                         };

            CaseHistoryResposnse chr = new CaseHistoryResposnse();
            chr.Id = request.CaseId;
            chr.CaseNoYear = CaseDetail.CaseNo + "/" + CaseDetail.CaseYear;
            chr.Title = CaseDetail.FirstTitle + " Vs " + CaseDetail.SecondTitle;
            chr.CourtType = courtDetail.CourtType;
            chr.Court = court.CourtBench_En;
            chr.History = workdt.ToList();
            return Result<CaseHistoryResposnse>.Success(chr);
        }
    }
}
