using AspNetCoreHero.Results;
using CourtApp.Application.DTOs.Registers;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using KT3Core.Areas.Global.Classes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Registers
{
    public class CaseSearchableDataQuery : IRequest<Result<List<InstitutionResponse>>>
    {
        public string UserId { get; set; }
        public Guid ClientId { get; set; }
        public string ReferalBy { get; set; }
        public string Status { get; set; }
    }
    public class CaseSearchableDataQueryHandler : IRequestHandler<CaseSearchableDataQuery, Result<List<InstitutionResponse>>>
    {
        private readonly IUserCaseRepository _caseRepo;
        private readonly IClientCacheRepository client;
        public CaseSearchableDataQueryHandler(IUserCaseRepository _caseRepo, IClientCacheRepository client)
        {
            this._caseRepo = _caseRepo;
            this.client = client;
        }
        public async Task<Result<List<InstitutionResponse>>> Handle(CaseSearchableDataQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<CaseDetailEntity>();
            if (predicate != null)
            {
                if (request.UserId != string.Empty)
                    predicate = predicate.And(c => c.CreatedBy.Equals(request.UserId));
                if (request.ClientId != Guid.Empty)
                    predicate = predicate.And(c => c.ClientId.Equals(request.ClientId));
                if (!string.IsNullOrEmpty(request.ReferalBy))
                {
                    var clientIds = (await client.GetCachedListAsync())
                        .Where(r => r.ReferalBy.Trim().Equals(request.ReferalBy.Trim().ToLower()))
                        .Select(s => s.Id).ToList();
                    predicate = predicate.And(c => clientIds.Contains(c.ClientId.Value));
                }
                Expression<Func<CaseDetailEntity, InstitutionResponse>> expression = e => new InstitutionResponse
                {
                    Id = e.Id,
                    CaseType = e.CaseType.Name_En,
                    No = e.CaseNo,
                    Year = e.CaseYear.ToString(),
                    Court = e.CourtBench.CourtBench_En,
                    FirstTitle = e.FirstTitle,
                    SecondTitle = e.SecondTitle,
                    InsititutionDate = e.InstitutionDate != default(DateTime) ? e.InstitutionDate.ToString("dd/MM/yyyy") : "-",
                };
                var dt = await _caseRepo.Entites
                .Include(c => c.CaseStage)
                .Include(c => c.CourtBench)
                .Where(predicate)
                .Select(expression)
                .ToListAsync();
                return await Result<List<InstitutionResponse>>.SuccessAsync(dt);
            }
            return await Result<List<InstitutionResponse>>.FailAsync("There are some problem while fetching the record");
        }
    }
}
