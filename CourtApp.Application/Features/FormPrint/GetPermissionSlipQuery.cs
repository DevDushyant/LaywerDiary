using AspNetCoreHero.Results;
using CourtApp.Application.DTOs.FormPrint;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace CourtApp.Application.Features.FormPrint
{
    public class GetPermissionSlipQuery : IRequest<Result<List<PermissionSlipResponse>>>
    {
        public List<Guid> CaseIds { get; set; }
    }
    public class GetPermissionSlipQueryHandler : IRequestHandler<GetPermissionSlipQuery, Result<List<PermissionSlipResponse>>>
    {
        private readonly IUserCaseRepository _CaseRepo;
        private readonly ICaseProceedingRepository _wRepo;
        private readonly ICaseAgainstRepository _wAgainstRepo;
        public GetPermissionSlipQueryHandler(IUserCaseRepository _CaseRepo, ICaseProceedingRepository _wRepo, ICaseAgainstRepository wAgainstRepo)
        {
            this._CaseRepo = _CaseRepo;
            this._wRepo = _wRepo;
            _wAgainstRepo = wAgainstRepo;
        }
        public async Task<Result<List<PermissionSlipResponse>>> Handle(GetPermissionSlipQuery request, CancellationToken cancellationToken)
        {
            var Cases = from cd in _CaseRepo.Entites
                       .Where(w => request.CaseIds.Contains(w.Id))
                        join p in _wRepo.Entities on cd.Id equals p.CaseId into CProcs
                        from cpd in CProcs.DefaultIfEmpty()
                        join ac in _wAgainstRepo.Entities on cd.Id equals ac.CaseId into AgCases
                        from acc in AgCases.DefaultIfEmpty()
                        select new PermissionSlipResponse
                        {
                            NoYear = cd.CaseNo + "/" + cd.CaseYear,
                            CaseType = cd.CaseType.Name_En,
                            Title = cd.FirstTitle + " Vs " + cd.SecondTitle,
                            DoP = cd.InstitutionDate.ToString("dd/MM/yyyy"),
                            MatterGo = cd.CaseStage.CaseStage,
                            DoI = acc.ImpugedOrderDate.ToString(),
                            NextDate = cpd.NextDate != null ? cpd.NextDate.Value.ToString("dd/MM/yyyy") : ""
                        };

            return Result<List<PermissionSlipResponse>>.Success(Cases.ToList());
        }
    }
}
