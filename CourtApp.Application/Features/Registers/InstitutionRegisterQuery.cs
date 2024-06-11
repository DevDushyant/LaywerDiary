using AspNetCoreHero.Results;
using CourtApp.Application.DTOs.Case;
using CourtApp.Application.DTOs.Registers;
using CourtApp.Application.Extensions;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using KT3Core.Areas.Global.Classes;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
namespace CourtApp.Application.Features.Registers
{
    public class InstitutionRegisterQuery : IRequest<PaginatedResult<InstitutionResponse>>
    {
        public DateTime FromDt { get; set; }
        public DateTime ToDt { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
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
                InsititutionDate = e.InstitutionDate != Convert.ToDateTime("0001-01-01") ? e.NextDate.Value.ToString("dd/MM/yyyy") : "-",
            };
            var predicate = PredicateBuilder.True<CaseDetailEntity>();
            if (predicate != null)
            {
                predicate = predicate.And(i => i.InstitutionDate >= request.FromDt && i.InstitutionDate <= request.ToDt);
            }
            var dt = await _caseRepo.Entites
                    .Where(predicate)
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return dt;
        }
    }
}
