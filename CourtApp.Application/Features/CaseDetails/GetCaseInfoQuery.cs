using AspNetCoreHero.Results;
using CourtApp.Application.Extensions;
using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using MediatR;
using System.Linq.Expressions;
using System;
using System.Threading;
using System.Threading.Tasks;
using KT3Core.Areas.Global.Classes;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace CourtApp.Application.Features.CaseDetails
{
    public class GetCaseInfoQuery : IRequest<PaginatedResult<GetCaseInfoDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string CaseNumber { get; set; }
        public int Year { get; set; }
        public string UserId { get; set; }
    }

    public class GetCaseInfoQueryHandler : IRequestHandler<GetCaseInfoQuery, PaginatedResult<GetCaseInfoDto>>
    {
        private readonly IUserCaseRepository _repository;
        private readonly ICaseProceedingRepository _ProcRepo;
        public GetCaseInfoQueryHandler(IUserCaseRepository _repository, ICaseProceedingRepository procRepo)
        {
            this._repository = _repository;
            _ProcRepo = procRepo;
        }
        public async Task<PaginatedResult<GetCaseInfoDto>> Handle(GetCaseInfoQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<CaseDetailEntity>();
            if (predicate != null)
            {
                if (request.UserId != null)
                    predicate = predicate.And(c => c.CreatedBy.Equals(request.UserId));
                if (request.Year != 0)
                    predicate = predicate.And(y => y.CaseYear == request.Year);
                if (request.CaseNumber != null && request.CaseNumber != string.Empty)
                    predicate = predicate.And(x => x.CaseNo == request.CaseNumber);
            }
            try
            {
                var cases = _repository
                    .Entites
                     .Include(ct => ct.CourtType)
                    .Include(ct => ct.CaseType)
                    .Include(cs => cs.CaseStage)
                    .Include(c => c.CourtBench)
                    .Where(predicate)
                    ;

                var caseIds = cases.Select(c => c.Id).ToList();
                var maxNextDates = _ProcRepo.Entities
                                    .Where(w => caseIds.Contains(w.CaseId))
                                    .GroupBy(x => x.CaseId)
                                    .Select(g => new
                                    {
                                        CaseId = g.Key,
                                        MaxNextDate = g.Max(x => x.NextDate)
                                    });
                var query = (from e in cases
                             join md in maxNextDates on e.Id equals md.CaseId into maxDates
                             from md in maxDates.DefaultIfEmpty()
                             select new GetCaseInfoDto
                             {
                                 Id = e.Id,
                                 CourtType = e.CourtType.CourtType.ToString(),
                                 CaseType = e.CaseType.Name_En,
                                 Court = e.CourtBench.CourtBench_En,
                                 CaseStage = e.CaseStage.CaseStage,
                                 DisposalDate = e.DisposalDate,
                                 OrderByKey = e.LastModifiedOn != null && e.LastModifiedOn > e.CreatedOn ? e.LastModifiedOn.Value : e.CreatedOn,
                                 CaseDetail = e.FirstTitle + " V/S " + e.SecondTitle + "(" + e.CaseNo + "/" + e.CaseYear + ")",
                                 NextDate = (e.NextDate.HasValue && md.MaxNextDate.HasValue)
                                             ? (e.NextDate.Value > md.MaxNextDate.Value ? e.NextDate.Value
                                                             : md.MaxNextDate.Value).ToString("dd/MM/yyyy")
                                             : (e.NextDate.HasValue ? e.NextDate.Value.ToString("dd/MM/yyyy")
                                             : (md.MaxNextDate.HasValue ? md.MaxNextDate.Value.ToString("dd/MM/yyyy") : ""))
                             }).OrderByDescending(o => o.OrderByKey);

                var result = await query.ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return result;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return null;
            }
        }
    }
}
