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
        public GetCaseDetailsQuery(int PageNumber, int PageSize)
        {
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
        }
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

            Expression<Func<CaseEntity, CaseDetailResponse>> expression = e => new CaseDetailResponse
            {
                Id = e.Id,
                CaseNumber = String.Concat(e.Number, " ", e.Year),
                CaseTypeName = e.TypeOfCase.Name_En,
                CourtName = e.Court.Name_En,
                FirstTitle = e.FirstTitle,
                SecondTitle = e.SecondTitle,
                NextHearingDate = e.NextDate.ToString("dd-MM-yyyy"),
                CaseKindName = e.CaseKind.CaseKind,
                CaseStage =""
            };

            var predicate = PredicateBuilder.True<CaseEntity>();
            if (predicate != null)
            {
                if (request.Year != 0)
                    predicate = predicate.And(y => y.Year == request.Year);
                if (request.CaseNumber != string.Empty)
                    predicate = predicate.And(x => x.Number == request.CaseNumber);
                //if (request.CourtId != Guid.Empty)
                //    predicate = predicate.And(x => x.CourtType.Id==request.CourtId);
                //if (request.CourtTypeId != Guid.Empty)
                //    predicate = predicate.And(x => x.CourtType.Id == request.CourtTypeId);

            }
            try
            {


                var paginatedList = await _RepoCase.Entites
                    .Include(c => c.Court)
                    .Include(c => c.TypeOfCase)
                    .Where(predicate)
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return paginatedList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return null;
        }
    }
}
