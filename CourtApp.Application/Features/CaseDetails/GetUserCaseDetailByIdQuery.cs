using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.Features.Case;
using CourtApp.Application.Features.CaseKinds.Query;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.UserCase
{
    public class GetUserCaseDetailByIdQuery : IRequest<Result<UserCaseDetailResponse>>
    {
        public Guid CaseId { get; set; }
        public string UserId { get; set; }
    }

    public class GetUserCaseDetailByIdQueryHandler : IRequestHandler<GetUserCaseDetailByIdQuery, Result<UserCaseDetailResponse>>
    {
        private readonly IUserCaseRepository _CaseRepo;
        private readonly IMapper _mapper;
        public GetUserCaseDetailByIdQueryHandler(IUserCaseRepository _CaseRepo, IMapper _mapper)
        {
            this._CaseRepo = _CaseRepo;
            this._mapper = _mapper;
        }
        public async Task<Result<UserCaseDetailResponse>> Handle(GetUserCaseDetailByIdQuery request, CancellationToken cancellationToken)
        {
            CaseDetailEntity detail = new CaseDetailEntity();
            //this condition is applicable for getting most
            //recent case for repeat the case.
            if (request.UserId != null && request.CaseId == Guid.Empty)
                detail = await _CaseRepo.GetMostRecentCaseInfo(request.UserId);
            else
                detail = await _CaseRepo.GetByIdAsync(request.CaseId);
            if (detail != null)
            {
                var mappeddata = _mapper.Map<UserCaseDetailResponse>(detail);
                if (detail.CourtType.Abbreviation == "HICT")
                {
                    mappeddata.IsHighCourt = true;
                    mappeddata.BenchId = detail.CourtBenchId;
                }
                else
                    mappeddata.CourtId = detail.CourtBenchId;

                if (detail.CaseAgainstEntities != null && detail.CaseAgainstEntities.Count > 0)
                {
                    var agl = new List<UpseartAgainstCaseDto>();
                    foreach (var item in detail.CaseAgainstEntities)
                    {
                        var agc = new UpseartAgainstCaseDto();
                        agc.ImpugedOrderDate = item.ImpugedOrderDate;
                        agc.StateId = item.StateId;
                        agc.CourtTypeId = item.CourtTypeId;
                        if (item.CourtType.Abbreviation == "HICT")
                        {
                            agc.IsAgHighCourt = true;
                            agc.BenchId = item.CourtBenchId;
                        }
                        else
                            agc.CourtId = item.CourtBenchId;
                        agc.CourtDistrictId = item.CourtDistrictId != null ? item.CourtDistrictId : Guid.Empty;
                        agc.ComplexId = item.ComplexId != null ? item.ComplexId : Guid.Empty;
                        agc.Cadre = item.Cadre;
                        agc.CaseNo = item.CaseNo;
                        agc.CaseCategoryId = item.CaseCategoryId;
                        agc.CaseTypeId = item.CaseTypeId;
                        agc.CaseYear = item.CaseYear;
                        agc.CisNo = item.CisNo;
                        agc.CisYear = item.CisYear;
                        agc.CnrNo = item.CnrNo;
                        agc.OfficerName = item.OfficerName;
                        agc.StrengthId = item.StrengthId;
                        agl.Add(agc);
                    }
                    mappeddata.AgainstCaseDetails = agl;
                }                
                return Result<UserCaseDetailResponse>.Success(mappeddata);
            }
            return Result<UserCaseDetailResponse>.Fail("Information is not exist!");
        }
    }
}
