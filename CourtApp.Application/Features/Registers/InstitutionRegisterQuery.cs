using AspNetCoreHero.Results;
using CourtApp.Application.DTOs.Case;
using CourtApp.Application.DTOs.Registers;
using CourtApp.Application.Extensions;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using KT3Core.Areas.Global.Classes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using static CourtApp.Application.Constants.Permissions;
namespace CourtApp.Application.Features.Registers
{
    public class InstitutionRegisterQuery : IRequest<PaginatedResult<InstitutionResponse>>
    {
        public DateTime FromDt { get; set; }
        public DateTime ToDt { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string UserId { get; set; }
    }
    public class InstitutionRegisterQueryHandler : IRequestHandler<InstitutionRegisterQuery, PaginatedResult<InstitutionResponse>>
    {
        private readonly IUserCaseRepository _caseRepo;
        public InstitutionRegisterQueryHandler(IUserCaseRepository _caseRepo)
        {
            this._caseRepo = _caseRepo;
        }
        public async Task<PaginatedResult<InstitutionResponse>> Handle(InstitutionRegisterQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<CaseDetailEntity, InstitutionResponse>> expression = e => new InstitutionResponse
            {
                Id = e.Id,
                CaseType = e.CaseType.Name_En,
                CourtType = e.CourtType.CourtType,
                CaseNo = String.Concat(e.CaseNo, "/", e.CaseYear),
                CourtBench = e.CourtBench.CourtBench_En,
                FirstTitle = e.FirstTitle,
                SecondTitle = e.SecondTitle,
                InsititutionDate = e.InstitutionDate != default(DateTime) ? e.InstitutionDate.ToString("dd/MM/yyyy") : "-",
            };
            var predicate = PredicateBuilder.True<CaseDetailEntity>();
            if (predicate != null)
            {
                if (request.UserId != string.Empty)
                    predicate = predicate.And(c => c.CreatedBy.Equals(request.UserId));

                predicate = predicate.And(i => i.InstitutionDate >= request.FromDt && i.InstitutionDate <= request.ToDt);
            }
            var dt = await _caseRepo.Entites
                .Include(c => c.CaseStage)
                .Include(c => c.CourtType)
                    .Where(predicate)
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return dt;
        }
    }
}
