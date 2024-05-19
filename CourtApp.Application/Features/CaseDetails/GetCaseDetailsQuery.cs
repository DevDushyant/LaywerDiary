using AspNetCoreHero.Results;
using CourtApp.Application.Constants;
using CourtApp.Application.DTOs.Case;
using CourtApp.Application.Extensions;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using KT3Core.Areas.Global.Classes;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private readonly ICaseStageCacheRepository _RepoStage;
        private readonly ICaseNatureCacheRepository _RepoNature;
        private readonly IUserCaseRepository _RepoCase;
        public GetCaseDetailsQueryHandler(ICaseNatureCacheRepository repoNature,
            IUserCaseRepository repoCase, ICourtTypeCacheRepository RepoCourtType,
            ICaseStageCacheRepository RepoStage)
        {
            _RepoNature = repoNature;
            _RepoCase = repoCase;
            _RepoCourtType = RepoCourtType;
            _RepoStage = RepoStage;
        }
        public async Task<PaginatedResult<CaseDetailResponse>> Handle(GetCaseDetailsQuery request, CancellationToken cancellationToken)
        {

            Expression<Func<CaseDetailEntity, CaseDetailResponse>> expression = e => new CaseDetailResponse
            {
                Id = e.Id,
                CaseTypeName = e.CaseType.Name_En,
                CourtType = e.CourtType.CourtType,
                CaseNumber = String.Concat(e.CaseNo, "/", e.CaseYear),
                CourtName = e.CourtBench.CourtBench_En,
                FirstTitle = e.FirstTitle,
                SecondTitle = e.SecondTitle,
                NextHearingDate = e.NextDate != Convert.ToDateTime("0001-01-01") ? e.NextDate.Value.ToString("dd/MM/yyyy") : "-",
                CaseStage = e.CaseStageCode,//StCodes.Where(s=>s.Key.Equals(e.CaseStageCode)).Select(s=>s.Value).FirstOrDefault(),
                CaseTitle = e.FirstTitle + " V/S " + e.SecondTitle + "(" + e.CaseNo + "/" + e.CaseYear + ")",
            };

            var predicate = PredicateBuilder.True<CaseDetailEntity>();
            if (predicate != null)
            {
                if (request.Year != 0)
                    predicate = predicate.And(y => y.CaseYear == request.Year);
                if (request.CaseNumber != string.Empty)
                    predicate = predicate.And(x => x.CaseNo == request.CaseNumber);

                if (request.CallingFrm == "HTD")
                    predicate = predicate.And(d => d.NextDate.Value.Date == DateTime.Now.Date);
                //if (request.CallingFrm == "BTD")
                  //  predicate = predicate.And(d => d.NextDate == null);

            }
            try
            {
                var dt = await _RepoCase.Entites
                    .Where(predicate)
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return null;
        }
    }
}
