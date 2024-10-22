using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Features.Case;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using CourtApp.Application.DTOs.CaseDetails;

namespace CourtApp.Application.Features.CaseDetails
{
    public class UpdateCaseDetailCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public int StrengthId { get; set; }
        public DateTime InstitutionDate { get; set; }
        public Guid CourtTypeId { get; set; }
        public Guid CaseCategoryId { get; set; }
        public Guid CaseTypeId { get; set; }
        public Guid CourtDistrictId { get; set; }
        public Guid CourtComplexId { get; set; }
        public Guid CourtBenchId { get; set; }
        public string CaseNo { get; set; }
        public int CaseYear { get; set; }
        public string FirstTitle { get; set; }
        public Guid FirstTitleCode { get; set; }
        public string SecondTitle { get; set; }
        public Guid SecoundTitleCode { get; set; }
        public string CisNumber { get; set; }
        public int CisYear { get; set; }
        public string CnrNumber { get; set; }
        public DateTime? NextDate { get; set; }
        public Guid CaseStageCode { get; set; }
        public Guid LinkedCaseId { get; set; }
        public Guid ClientId { get; set; }
        public int StateId { get; set; }
        public ICollection<UpseartAgainstCaseDto> AgainstCaseDetails { get; set; }
    }
    public class UpdateCaseDetailCommandHandler : IRequestHandler<UpdateCaseDetailCommand, Result<Guid>>
    {
        private readonly IUserCaseRepository _repository;
        private readonly IMapper _mapper;
        private IUnitOfWork _uow { get; set; }
        public UpdateCaseDetailCommandHandler(IUserCaseRepository _repository, IUnitOfWork _uow, IMapper _mapper)
        {
            this._repository = _repository;
            this._uow = _uow;
            this._mapper = _mapper;
        }

        public async Task<Result<Guid>> Handle(UpdateCaseDetailCommand request, CancellationToken cancellationToken)
        {
            var detail = await _repository.GetByIdAsync(request.Id);
            if (detail == null)
                return Result<Guid>.Fail($"Case detail Not Found.");
            else
            {
                detail.InstitutionDate = request.InstitutionDate;
                detail.StateId = request.StateId;
                detail.CourtTypeId = request.CourtTypeId;
                detail.CourtBenchId = request.CourtBenchId != Guid.Empty ? request.CourtBenchId : null;
                detail.CourtDistrictId = request.CourtDistrictId != Guid.Empty ? request.CourtDistrictId : null;
                detail.CourtComplexId = request.CourtComplexId != Guid.Empty ? request.CourtComplexId : null;
                detail.CaseCategoryId = request.CaseCategoryId;
                detail.CaseStageId = request.CaseStageCode;
                detail.CaseYear = request.CaseYear;
                detail.NextDate = request.NextDate;
                detail.CnrNumber = request.CnrNumber;
                detail.CisNumber = request.CisNumber;
                detail.CaseYear = request.CaseYear;
                detail.CaseNo = request.CaseNo;
                detail.CaseCategoryId = request.CaseCategoryId;
                detail.CaseTypeId = request.CaseTypeId;
                detail.STitleId = request.SecoundTitleCode;
                detail.FTitleId = request.FirstTitleCode;
                detail.FirstTitle = request.FirstTitle;
                detail.SecondTitle = request.SecondTitle;
                detail.StrengthId = request.StrengthId;
                detail.CaseTypeId = request.CaseTypeId;
                if (request.AgainstCaseDetails.Count > 0)
                {
                    foreach (var item in request.AgainstCaseDetails)
                    {
                        if (item.Id == Guid.Empty && item.StateId!=0)
                        {
                            CaseDetailAgainstEntity againstDt = new CaseDetailAgainstEntity();
                            againstDt.ImpugedOrderDate = item.ImpugedOrderDate.Value;
                            againstDt.StateId = item.StateId.Value;
                            againstDt.CourtTypeId = item.CourtTypeId.Value;                           
                            againstDt.CaseYear = item.CaseYear.Value;
                            againstDt.CaseNo = item.CaseNo;
                            againstDt.CaseCategoryId = item.CaseCategoryId.Value;
                            againstDt.CaseTypeId = item.CaseTypeId.Value;
                            againstDt.StrengthId = item.StrengthId != null ? item.StrengthId.Value : 0;
                            againstDt.OfficerName = item.OfficerName;
                            againstDt.CisYear = item.CisYear.Value;
                            againstDt.CisNo = item.CisNumber;
                            againstDt.CaseId = request.Id;
                            againstDt.Id = Guid.NewGuid();
                            againstDt.CourtBenchId = item.BenchId != Guid.Empty ? item.BenchId : null;
                            againstDt.CourtDistrictId = item.CourtDistrictId != Guid.Empty ? item.CourtDistrictId : null;
                            againstDt.CourtComplexId = item.ComplexId != Guid.Empty ? item.ComplexId : null;
                            detail.CaseAgainstEntities.Add(againstDt);
                        }
                        else
                        {
                            var againstDt = detail.CaseAgainstEntities.Where(w => w.Id == item.Id).FirstOrDefault();
                            if (againstDt != null)
                            {
                                againstDt.ImpugedOrderDate = item.ImpugedOrderDate.Value;
                                againstDt.StateId = item.StateId.Value;
                                againstDt.CourtTypeId = item.CourtTypeId.Value;
                                againstDt.CourtBenchId = item.BenchId;
                                againstDt.CaseYear = item.CaseYear.Value;
                                againstDt.CaseNo = item.CaseNo;
                                againstDt.CaseCategoryId = item.CaseCategoryId.Value;
                                againstDt.CaseTypeId = item.CaseTypeId.Value;
                                againstDt.StrengthId = item.StrengthId != null ? item.StrengthId.Value : 0;
                                againstDt.OfficerName = item.OfficerName;
                                againstDt.CisYear = item.CisYear.Value  ;
                                againstDt.CisNo = item.CisNumber;
                                againstDt.CaseId = request.Id;
                                againstDt.Id = new Guid();
                                againstDt.CourtBenchId = item.BenchId != Guid.Empty ? item.BenchId : null;
                                againstDt.CourtDistrictId = item.CourtDistrictId != Guid.Empty ? item.CourtDistrictId : null;
                                againstDt.CourtComplexId = item.ComplexId != Guid.Empty ? item.ComplexId : null;
                                detail.CaseAgainstEntities.Add(againstDt);
                            }
                        }
                    }
                }
                await _repository.UpdateAsync(detail);
                await _uow.Commit(cancellationToken);
                return Result<Guid>.Success(detail.Id);
            }
        }
    }
}
