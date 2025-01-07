﻿using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace CourtApp.Application.Features.CaseDetails
{
    public class GetCaseDetailInfoQuery : IRequest<Result<CaseDetailInfoDto>>
    {
        public Guid CaseId { get; set; }
    }
    public class GetCaseDetailInfoQueryHandler : IRequestHandler<GetCaseDetailInfoQuery, Result<CaseDetailInfoDto>>
    {
        private readonly IUserCaseRepository _CaseRepo;
        private readonly IMapper _Mapper;
        public GetCaseDetailInfoQueryHandler(IUserCaseRepository _CaseRepo, IMapper _Mapper)
        {
            this._CaseRepo = _CaseRepo;
            this._Mapper = _Mapper;
        }
        public async Task<Result<CaseDetailInfoDto>> Handle(GetCaseDetailInfoQuery request, CancellationToken cancellationToken)
        {
            var detail = await _CaseRepo.GetDetailAsync(request.CaseId);
            CaseDetailInfoDto ct = new CaseDetailInfoDto();
            if (detail != null)
            {
                ct.Id = request.CaseId;
                ct.InstitutionDate = detail.InstitutionDate.Date.ToString("dd/MM/yyyy");
                ct.State = detail.State.Name_En;
                ct.CourtType = detail.CourtType.CourtType;
                ct.CourtBench = detail.CourtBench.CourtBench_En;
                ct.CaseNo = detail.CaseNo;
                ct.CaseYear = detail.CaseYear.ToString();
                ct.CaseCategory = detail.CaseCategory.Name_En;
                ct.CaseType = detail.CaseType.Name_En;
                ct.FirstTitle = detail.FirstTitle;
                ct.FirstTitleDetail = detail.FTitle.Name_En;
                ct.SecondTitle = detail.SecondTitle; 
                ct.SecondTitleDetail = detail.STitle.Name_En;
                ct.CaseStage = detail.CaseStage.CaseStage;
                ct.CisNo = detail.CisNumber;
                ct.CisYear = detail.CisYear.ToString();
                ct.CnrNo = detail.CnrNumber;
                ct.DistrictCourt = detail.CourtDistrict!=null? detail.CourtDistrict.Name_En:"";
                ct.CourtComplex = detail.Complex !=null?detail.Complex.Name_En :"";
                ct.NextDate = detail.NextDate != null ? detail.NextDate.Value.ToString("dd/MM/yyyy") : "";
                var againstDetail = detail.CaseAgainstEntities;
                if (againstDetail != null && againstDetail.Count > 0)
                {
                    ct.IsCaseAgainstDecision = true;
                    var aCaseDetails = new List<AgainstCaseDetail>();
                    foreach (var item in againstDetail)
                    {
                        aCaseDetails.Add(new AgainstCaseDetail
                        {
                            ImpugedOrder = item.ImpugedOrderDate.ToString("dd/MM/yyyy"),
                            State = item.State.Name_En,
                            CourtBench = item.CourtBench != null ? item.CourtBench.CourtBench_En : "",
                            CourtType = item.CourtType.CourtType.ToString(),
                            CaseNo = item.CaseNo != null ? item.CaseNo.ToString() : "",
                            CaseYear = item.CaseYear.ToString(),
                            CisNo = item.CisNo != null ? item.CisNo.ToString() : "",
                            CisYear =  item.CisYear.ToString(),
                            CnrNo = item.CnrNo != null ? item.CnrNo.ToString() : "",
                            Cadre = item.Cadre!=null ? item.Cadre.Name_En.ToString():"",
                            OfficerName = item.OfficerName != null ? item.OfficerName:"",
                            CaseCategory = item.CaseCategory != null ? item.CaseCategory.Name_En : "",
                            CourtComplex = item.Complex != null ? item.Complex.Name_En : "",
                            CaseType = item.CaseType != null ? item.CaseType.Name_En : "",
                            DistrictCourt = item.CourtDistrict != null ? item.CourtDistrict.Name_En : "",
                        });
                    }
                    ct.AgainstCases = aCaseDetails;
                }
                else
                    ct.IsCaseAgainstDecision = false;

            }
            return Result<CaseDetailInfoDto>.Success(ct);
        }
    }
}
