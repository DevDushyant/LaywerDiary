using AspNetCoreHero.Results;
using CourtApp.Application.DTOs.CourtMaster;
using CourtApp.Application.DTOs.Registers;
using CourtApp.Application.Extensions;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using KT3Core.Areas.Global.Classes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Registers
{
    public class OtherRegisterQuery : IRequest<PaginatedResult<OtherRegisterResponse>>
    {
        public DateTime FromDt { get; set; }
        public DateTime ToDt { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string UserId { get; set; }
    }
    public class OtherRegisterQueryHandler : IRequestHandler<OtherRegisterQuery, PaginatedResult<OtherRegisterResponse>>
    {
        private readonly IUserCaseRepository _caseRepo;
        private readonly ICaseWorkRepository _wRepo;
        public OtherRegisterQueryHandler(IUserCaseRepository _caseRepo, ICaseWorkRepository _wRepo)
        {
            this._caseRepo = _caseRepo;
            this._wRepo = _wRepo;
        }
        public Task<PaginatedResult<OtherRegisterResponse>> Handle(OtherRegisterQuery request, CancellationToken cancellationToken)
        {

            Expression<Func<CaseWorkEntity, OtherRegisterResponse>> expression = e => new OtherRegisterResponse
            {
                Id = e.CaseId,
                Title = e.Case.CaseType.Abbreviation + "(" + e.Case.CaseNo.ToString() + "/" + e.Case.CaseYear.ToString() + ")" + e.Case.FirstTitle + "Vs" + e.Case.SecondTitle,
                WorkDate = e.Status == 1 && e.LastModifiedOn != null ? e.LastModifiedOn.Value.ToString("dd/MM/yyyy") : "",
                WorkDone = e.Work.Name_En,
                WorkType = e.WorkType.Work_En
            };
            var predicate = PredicateBuilder.True<CaseWorkEntity>();
            DateTime to = DateTime.Now;
            if (request.ToDt != default(DateTime)) to = request.ToDt;
            if (request.FromDt != default(DateTime))
                predicate = predicate.And(b => b.LastModifiedOn.Value >= request.FromDt && b.LastModifiedOn.Value <= to);
            predicate = predicate.And(w => w.WorkType.Abbreviation != "COPY" && w.WorkType.Abbreviation != "DISP");
            var fndt = _wRepo.Entities.Where(predicate)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return fndt;
        }
    }
}
