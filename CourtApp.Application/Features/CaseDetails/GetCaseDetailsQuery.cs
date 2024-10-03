using AspNetCoreHero.Results;
using CourtApp.Application.DTOs.Case;
using CourtApp.Application.Extensions;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using KT3Core.Areas.Global.Classes;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.UserCase
{
    public class GetCaseDetailsQuery : IRequest<PaginatedResult<CaseDetailResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string CaseNumber { get; set; } = string.Empty;
        public int Year { get; set; }
        public int StateCode { get; set; }
        public int DistrictCode { get; set; }
        public Guid CourtTypeId { get; set; }
        public Guid CourtId { get; set; }
        public Guid CaseTyepId { get; set; }
        public DateTime HearingDate { get; set; }
        public string CallingFrm { get; set; }
    }
    public class GetCaseDetailsQueryHandler : IRequestHandler<GetCaseDetailsQuery, PaginatedResult<CaseDetailResponse>>
    {
        private readonly ICourtTypeCacheRepository _RepoCourtType;
        private readonly ICourtTypeRepository _RepoCrtType;
        private readonly ICaseStageCacheRepository _RepoStage;
        private readonly ICaseNatureCacheRepository _RepoNature;
        private readonly IUserCaseRepository _RepoCase;
        private readonly ICaseProceedingRepository _RepoProceeding;
        private readonly IFSTitleCacheRepository _RepoFSTitle;
        private readonly ICourtBenchRepository _RepoCourtBench;
        public GetCaseDetailsQueryHandler(ICaseNatureCacheRepository repoNature,
            IUserCaseRepository repoCase, ICourtTypeCacheRepository RepoCourtType,
            ICaseStageCacheRepository RepoStage, ICaseProceedingRepository repoProceeding,
            IFSTitleCacheRepository repoFSTitle, ICourtBenchRepository repoCourtBench, ICourtTypeRepository repoCrtType)
        {
            _RepoNature = repoNature;
            _RepoCase = repoCase;
            _RepoCourtType = RepoCourtType;
            _RepoStage = RepoStage;
            _RepoProceeding = repoProceeding;
            _RepoFSTitle = repoFSTitle;
            _RepoCourtBench = repoCourtBench;
            _RepoCrtType = repoCrtType;
        }
        public async Task<PaginatedResult<CaseDetailResponse>> Handle(GetCaseDetailsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<CaseDetailEntity, CaseDetailResponse>> expression = e => new CaseDetailResponse
            {
                Id = e.Id,
                FTitleType = e.FTitle.Name_En,
                STitleType = e.FTitle.Name_En,
                CaseTypeName = e.CaseType.Name_En,
                CourtType = e.CourtType.CourtType,
                CaseNumber = e.CaseNo==null? e.CaseYear.ToString(): String.Concat(e.CaseNo, "/", e.CaseYear),
                CourtName = e.CourtBench.CourtBench_En,
                FirstTitle = e.FirstTitle,
                SecondTitle = e.SecondTitle,
                NextHearingDate = e.NextDate.HasValue == true ? e.NextDate.Value : default(DateTime),
                CaseStage = e.CaseStage.CaseStage,
                CaseTitle = e.FirstTitle + " V/S " + e.SecondTitle + "(" + e.CaseNo + "/" + e.CaseYear + ")",
            };
            var predicate = PredicateBuilder.True<CaseDetailEntity>();
            if (predicate != null)
            {
                if (request.Year != 0)
                    predicate = predicate.And(y => y.CaseYear == request.Year);
                if (request.CaseNumber != string.Empty)
                    predicate = predicate.And(x => x.CaseNo == request.CaseNumber);
            }
            IQueryable<CaseProcedingEntity> proceedingData = _RepoProceeding.Entities;
            if (request.HearingDate! != default(DateTime))
                proceedingData = _RepoProceeding.Entities
                    .Where(n => n.NextDate.Value == request.HearingDate);

            var td = _RepoCase.Entites.Where(predicate)
                .Select(expression);
            IQueryable<CaseDetailResponse> fnldt;
            if (proceedingData.Count() > 0)
            {
                fnldt = from cd in td
                        let MaxDt = (from cp in proceedingData
                                     where cp.CaseId == cd.Id
                                     orderby cp.NextDate.Value descending
                                     select cp.NextDate.Value).FirstOrDefault()
                        select new CaseDetailResponse
                        {
                            Id = cd.Id,
                            CourtName = cd.CourtName,
                            Abbreviation = cd.Abbreviation,
                            CaseTypeName = cd.CaseTypeName,
                            FTitleType = cd.FTitleType,
                            FirstTitle = cd.FirstTitle,
                            STitleType = cd.STitleType,
                            SecondTitle = cd.SecondTitle,
                            CaseStage = cd.CaseStage,
                            CaseNumber = cd.CaseNumber,
                            NextHearingDate = MaxDt,
                            CaseTitle = cd.CaseTitle
                        };
            }
            else
            {
                fnldt = from cd in td.Where(w => w.NextHearingDate == request.HearingDate)select cd;
            }

            var dt = from cd in fnldt
                     select new CaseDetailResponse
                     {
                         Id = cd.Id,
                         CourtName = cd.CourtName,
                         Abbreviation = cd.Abbreviation,
                         CaseTypeName = cd.CourtType,
                         FTitleType = cd.FTitleType,
                         FirstTitle = cd.FirstTitle,
                         STitleType = cd.STitleType,
                         SecondTitle = cd.SecondTitle,
                         CaseStage = cd.CaseStage,
                         CaseNumber = cd.CaseNumber,
                         NextHearingDate = cd.NextHearingDate,
                         CaseTitle = cd.CaseTitle
                     };
            if (request.HearingDate! != default(DateTime))
            {
                var d = from e in dt where e.NextHearingDate == request.HearingDate select e;
                return await d.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            }
            return await dt.ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
