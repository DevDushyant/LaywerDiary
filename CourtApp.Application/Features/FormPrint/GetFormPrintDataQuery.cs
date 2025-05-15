using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.DTOs.FormPrint;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourtApp.Application.Features.FormPrint
{
    public class GetFormPrintDataQuery : IRequest<Result<List<GlobalFormPrintDto>>>
    {
        public List<Guid> CaseIds { get; set; }
    }
    public class GetFormPrintDataQueryHandler : IRequestHandler<GetFormPrintDataQuery, Result<List<GlobalFormPrintDto>>>
    {

        private readonly IUserCaseRepository _CaseRepo;
        private readonly ICaseProceedingRepository _wRepo;
        private readonly IMapper _mapper;
        private readonly IClientRepository _ClientRepo;
        private readonly IFSTitleRepository _AppeareceRepo;
        private readonly ICaseTitleRepository _TitleRepo;
        public GetFormPrintDataQueryHandler(IUserCaseRepository _CaseRepo, IMapper _mapper,
            ICaseProceedingRepository wRepo, IClientRepository clientRepo, IFSTitleRepository _AppeareceRepo, ICaseTitleRepository _TitleRepo)
        {
            this._CaseRepo = _CaseRepo;
            this._mapper = _mapper;
            _wRepo = wRepo;
            _ClientRepo = clientRepo;
            this._AppeareceRepo = _AppeareceRepo;
            this._TitleRepo = _TitleRepo;
        }
        public async Task<Result<List<GlobalFormPrintDto>>> Handle(GetFormPrintDataQuery request, CancellationToken cancellationToken)
        {

            var caseData = (
                        from caseInfo in _CaseRepo.Entites
                            .AsNoTracking()
                            .Include(c => c.CaseCategory)
                            .Include(c => c.State)
                            .Include(c => c.CaseStage)
                            .Include(c => c.CourtType)
                            .Include(c => c.FTitle)
                            .Include(c => c.STitle)
                            .Include(c => c.CaseProcEntities)
                            .Include(ac => ac.CaseAgainstEntities).ThenInclude(c=>c.CourtBench)
                            .Include(ac => ac.CaseAgainstEntities).ThenInclude(c=>c.CourtDistrict)
                            .Include(ac => ac.CaseAgainstEntities).ThenInclude(c=>c.CaseCategory)
                            .Include(ac => ac.CaseAgainstEntities).ThenInclude(c=>c.CaseType)
                            .Include(ac => ac.CaseAgainstEntities).ThenInclude(c=>c.CourtType)
                            .Include(ac => ac.CaseAgainstEntities).ThenInclude(c=>c.Complex)                            
                            .Include(ac => ac.CaseAgainstEntities).ThenInclude(c=>c.Cadre)
                            .Where(w => request.CaseIds.Contains(w.Id))
                        join title in _TitleRepo.Titles.AsNoTracking().Where(t => t.TypeId == 2)
                            .Include(t => t.CaseApplicants)
                            on caseInfo.Id equals title.CaseId into CompleteTitle
                        from ct in CompleteTitle.DefaultIfEmpty()
                        let cd = caseInfo
                        select new
                        {
                            CaseInfo = caseInfo,
                            Title = ct,
                            AgainstCaseDetail = caseInfo.CaseAgainstEntities,
                            CaseApplicants = ct != null && ct.TypeId == 2 ? ct.CaseApplicants : null
                        }
                    )
                    .AsEnumerable()
                    .Select(data => new GlobalFormPrintDto
                    {
                        InstitutionDate = data.CaseInfo.InstitutionDate.ToString("dd/MM/yyyy"),
                        State = data.CaseInfo.State.Name_En,
                        CourtType = data.CaseInfo.CourtType?.CourtType ?? "",
                        CourtDistrict=data.CaseInfo.CourtDistrict?.Name_En??"",
                        CourtComplex=data.CaseInfo.Complex?.Name_En??"",
                        Court=data.CaseInfo.CourtBench?.CourtBench_En??"",
                        Strength="",
                        CaseNoYear = data.CaseInfo.CaseNo + "/" + data.CaseInfo.CaseYear,
                        CaseCategory = data.CaseInfo.CaseCategory?.Name_En ?? "",
                        CaseType=data.CaseInfo.CaseType?.Name_En??"",
                        CisNoYear= data.CaseInfo.CisNumber +"/"+ data.CaseInfo.CisYear,
                        PetitionerAppearance = data.CaseInfo.FTitle?.Name_En ?? "",
                        Petitioner = data.CaseInfo.FirstTitle,
                        RespondantAppearance = data.CaseInfo.STitle?.Name_En ?? "",
                        Respondent = data.CaseInfo.SecondTitle,
                        NextDate = GetLatestNextDate(data.CaseInfo),                        
                        CaseStage = data.CaseInfo.CaseStage?.CaseStage ?? "",
                        CnrNo=data.CaseInfo.CnrNumber,
                        DisposalDate = data.CaseInfo.DisposalDate?.ToString("dd/MM/yyyy") ?? "",
                        AgainstCourtDetail = GetAgainsCaseDetail(data.CaseInfo),
                        Applicants = data.CaseApplicants?.Select(s => new ApplicantDetailDto
                        {
                            Applicant = s.ApplicantDetail,
                            ApplicantNo = s.ApplicantNo
                        }).ToList(),
                        

                    }).ToList();

            return await Result<List<GlobalFormPrintDto>>.SuccessAsync(caseData);
        }

        private AgainstCaseDetail GetAgainsCaseDetail(CaseDetailEntity caseInfo)
        {
            if (caseInfo?.CaseAgainstEntities == null || !caseInfo.CaseAgainstEntities.Any())
            {
                return null;
            }

            var agstCaseDetail = caseInfo.CaseAgainstEntities.Select(s => new AgainstCaseDetail
            {
                ImpugedOrder = s.ImpugedOrderDate.ToString("dd/MM/yyyy"),
                State = s.State?.Name_En,
                CourtType = s.CourtType?.CourtType.ToString(),
                CourtDistrict= s.CourtDistrict?.Name_En ?? "",
                CourtComplex = s.Complex?.Name_En ?? "",
                CourtBench = s.CourtBench?.CourtBench_En ?? "",
                CaseNo = s.CaseNo?.ToString() ?? "",
                CaseYear = s.CaseYear.ToString(),
                CaseType = s.CaseType?.Name_En ?? "",
                CisNo = s.CisNo?.ToString() ?? "",
                CisYear = s.CisYear.ToString(),
                CnrNo = s.CnrNo?.ToString() ?? "",
                Cadre = s.Cadre?.Name_En ?? "",
                OfficerName = s.OfficerName ?? "",
                CaseCategory = s.CaseCategory?.Name_En ?? "",
                DistrictCourt = s.CourtDistrict?.Name_En ?? "",
            }).FirstOrDefault();
            return agstCaseDetail;
        }


        private string GetLatestNextDate(CaseDetailEntity caseInfo)
        {
            var procDates = caseInfo.CaseProcEntities?
                .Where(p => p.NextDate.HasValue)
                .Select(p => p.NextDate.Value)
                .ToList();

            var maxProcDate = procDates?.Any() == true ? procDates.Max() : DateTime.MinValue;

            if (caseInfo.NextDate.HasValue && caseInfo.NextDate > maxProcDate)
                return caseInfo.NextDate.Value.ToString("dd/MM/yyyy");

            return maxProcDate != DateTime.MinValue ? maxProcDate.ToString("dd/MM/yyyy") : "";
        }

    }
}
