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
        #region Common Properties Among all Court Type
        public DateTime InstitutionDate { get; set; }
        public int StateId { get; set; }
        public Guid CourtTypeId { get; set; }
        public Guid CaseCategoryId { get; set; }
        public Guid CaseTypeId { get; set; }
        public string CaseNo { get; set; }
        public int? CaseYear { get; set; }
        public string FirstTitle { get; set; }
        public Guid FTitleId { get; set; }
        public string SecondTitle { get; set; }
        public Guid STitleId { get; set; }
        public string CisNumber { get; set; }
        public int CisYear { get; set; }
        public string CnrNumber { get; set; }
        public DateTime? NextDate { get; set; }
        public Guid? CaseStageId { get; set; }
        public Guid? LinkedCaseId { get; set; }
        public Guid? ClientId { get; set; }
        public List<UpseartAgainstCaseDto> AgainstCaseDetails { get; set; }
        #endregion

        #region Other than High Court Propeties
        public Guid? CourtDistrictId { get; set; }
        public Guid? ComplexId { get; set; }
        public Guid? CourtId { get; set; }

        #endregion

        #region HighCourt Properties        
        public int? StrengthId { get; set; }
        public Guid? BenchId { get; set; }
        #endregion
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
                detail.CourtBenchId = request.BenchId != null ? request.BenchId.Value : request.CourtId.Value;
                detail.CourtDistrictId = request.CourtDistrictId != Guid.Empty ? request.CourtDistrictId : null;
                detail.ComplexId = request.ComplexId != Guid.Empty ? request.ComplexId : null;
                detail.CaseCategoryId = request.CaseCategoryId;
                detail.CaseStageId = request.CaseStageId!=null? request.CaseStageId.Value:null;
                detail.CaseYear = request.CaseYear.Value;
                detail.NextDate = request.NextDate != null ? request.NextDate.Value : DateTime.MinValue;
                detail.CnrNumber = request.CnrNumber;
                detail.CisNumber = request.CisNumber;
                detail.CaseYear = request.CaseYear != null ? request.CaseYear.Value : 0;
                detail.CaseNo = request.CaseNo;
                detail.CaseCategoryId = request.CaseCategoryId;
                detail.CaseTypeId = request.CaseTypeId;
                detail.STitleId = request.STitleId;
                detail.FTitleId = request.FTitleId;
                detail.FirstTitle = request.FirstTitle;
                detail.SecondTitle = request.SecondTitle;
                detail.StrengthId = request.StrengthId != null ? request.StrengthId.Value : 0;
                detail.CaseTypeId = request.CaseTypeId;
                if (request.AgainstCaseDetails.Count > 0)
                {
                    foreach (var item in request.AgainstCaseDetails)
                    {
                        if (item.CaseId == null && item.StateId != 0)
                        {
                            CaseDetailAgainstEntity againstDt = new CaseDetailAgainstEntity();
                            againstDt.ImpugedOrderDate = item.ImpugedOrderDate.Value;
                            againstDt.CourtBenchId = item.BenchId != null ? item.BenchId.Value : item.CourtId.Value;
                            againstDt.StateId = item.StateId.Value;
                            againstDt.CourtTypeId = item.CourtTypeId.Value;
                            againstDt.CaseYear = item.CaseYear.Value;
                            againstDt.CaseNo = item.CaseNo;
                            againstDt.CaseCategoryId = item.CaseCategoryId.Value;
                            againstDt.CaseTypeId = item.CaseTypeId!=null? item.CaseTypeId.Value:Guid.Empty;
                            againstDt.StrengthId = item.StrengthId != null ? item.StrengthId.Value : 0;
                            againstDt.OfficerName = item.OfficerName;
                            againstDt.CisYear = item.CisYear.Value;
                            againstDt.CisNo = item.CisNo;
                            againstDt.CaseId = request.Id;
                            againstDt.CourtDistrictId = item.CourtDistrictId != Guid.Empty ? item.CourtDistrictId : null;
                            againstDt.ComplexId = item.ComplexId != Guid.Empty ? item.ComplexId : null;
                            detail.CaseAgainstEntities.Add(againstDt);
                        }
                        else
                        {
                            var againstDt = detail.CaseAgainstEntities.Where(w => w.CaseId == item.CaseId).FirstOrDefault();
                            if (againstDt != null)
                            {
                                againstDt.ImpugedOrderDate = item.ImpugedOrderDate.Value;
                                againstDt.StateId = item.StateId.Value;
                                againstDt.CourtTypeId = item.CourtTypeId.Value;
                                againstDt.CourtBenchId = item.BenchId != null ? item.BenchId.Value : item.CourtId.Value;
                                againstDt.CaseYear = item.CaseYear.Value;
                                againstDt.CaseNo = item.CaseNo;
                                againstDt.CaseCategoryId = item.CaseCategoryId.Value;
                                againstDt.CaseTypeId = item.CaseTypeId.Value;
                                againstDt.StrengthId = item.StrengthId != null ? item.StrengthId.Value : 0;
                                againstDt.OfficerName = item.OfficerName;
                                againstDt.CisYear = item.CisYear.Value;
                                againstDt.CisNo = item.CisNo;
                                againstDt.CaseId = request.Id;
                                againstDt.CourtDistrictId = item.CourtDistrictId != Guid.Empty ? item.CourtDistrictId : null;
                                againstDt.ComplexId = item.ComplexId != Guid.Empty ? item.ComplexId : null;
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
