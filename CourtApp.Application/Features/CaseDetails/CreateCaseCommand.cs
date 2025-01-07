using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Application.DTOs.CaseDetails;
using KT3Core.Areas.Global.Classes;

namespace CourtApp.Application.Features.Case
{
    public class CreateCaseCommand : IRequest<Result<Guid>>
    {
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
        public Guid? LCaseId { get; set; }
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
    public class CaseAgainstEntityModel
    {
        public Guid Id { get; set; }
        public DateTime ImpugedOrderDate { get; set; }
        public Guid CourtTypeId { get; set; }
        public Guid? CourtBenchId { get; set; }
        public Guid CaseCategoryId { get; set; }
        public Guid CaseTypeId { get; set; }
        public int StateId { get; set; }
        public int? StrengthId { get; set; }
        public string CaseNo { get; set; }
        public int CaseYear { get; set; }
        public int CisYear { get; set; }
        public string OfficerName { get; set; }
        public Guid Cadre { get; set; }
        public Guid? CourtDistrictId { get; set; }
        public Guid? AgainstBenchId { get; set; }
        public Guid? ComplexId { get; set; }
        public Guid? CourtId { get; set; }
        public string CisNumber { get; set; }
        public string CnrNumber { get; set; }
        public bool IsAgHighCourt { get; set; }

    }

    public class CreateCaseManagmentCommandHandler : IRequestHandler<CreateCaseCommand, Result<Guid>>
    {
        private readonly IUserCaseRepository _Repository;
        private readonly IMapper _mapper;
        private readonly ICaseNatureCacheRepository _CaseNatureCacheRepository;
        private readonly ICaseKindCacheRepository _CaseKindCacheRepository;
        private readonly IClientCacheRepository _ClientCacheRepository;
        private readonly ICaseStageCacheRepository _CaseStageCacheRepository;
        private readonly ICourtTypeCacheRepository _CourtTypeCacheRepository;
        private readonly ICourtMasterCacheRepository _CourtMasterCacheRepository;
        private readonly ITypeOfCasesCacheRepository _ITypeOfCasesCacheRepository;
        private readonly ICaseAgainstRepository _CaseAgainstRepo;

        private IUnitOfWork _unitOfWork { get; set; }
        public CreateCaseManagmentCommandHandler(IUserCaseRepository _Repository,
            IMapper _mapper, IUnitOfWork _unitOfWork,
            ICaseKindCacheRepository caseKindCacheRepository,
            IClientCacheRepository clientCacheRepository,
            ICaseStageCacheRepository caseStageCacheRepository,
            ICourtTypeCacheRepository _CourtTypeCacheRepository,
            ITypeOfCasesCacheRepository _ITypeOfCasesCacheRepository,
            ICourtMasterCacheRepository _CourtMasterCacheRepository,
            ICaseNatureCacheRepository _CaseNatureCacheRepository,
            ICaseAgainstRepository _CaseAgainstRepo
            )
        {
            this._mapper = _mapper;
            this._Repository = _Repository;
            this._unitOfWork = _unitOfWork;
            _CaseKindCacheRepository = caseKindCacheRepository;
            _ClientCacheRepository = clientCacheRepository;
            _CaseStageCacheRepository = caseStageCacheRepository;
            this._CourtTypeCacheRepository = _CourtTypeCacheRepository;
            this._ITypeOfCasesCacheRepository = _ITypeOfCasesCacheRepository;
            this._CourtMasterCacheRepository = _CourtMasterCacheRepository;
            this._CaseNatureCacheRepository = _CaseNatureCacheRepository;
            this._CaseAgainstRepo = _CaseAgainstRepo;
        }
        public async Task<Result<Guid>> Handle(CreateCaseCommand request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<CaseDetailEntity>();
            if (predicate != null)
            {
                if (request.CourtTypeId != Guid.Empty)
                    predicate = predicate.And(y => y.CourtTypeId == request.CourtTypeId);
                if (request.CaseTypeId != Guid.Empty)
                    predicate = predicate.And(y => request.CaseTypeId == request.CaseTypeId);
                if (request.CaseNo != null && request.CaseYear != 0)
                    predicate = predicate.And(y => y.CaseNo.Equals(request.CaseNo) && y.CaseYear == request.CaseYear);
                if (request.CaseNo == null && request.FirstTitle != "" && request.SecondTitle != "")
                    predicate = predicate.And(y => y.FirstTitle.Equals(request.FirstTitle) && y.SecondTitle.Equals(request.SecondTitle));

            }
            var isCaseExist = _Repository.Entites.Where(predicate).FirstOrDefault();
            if (isCaseExist == null)
            {
                var entity = _mapper.Map<CaseDetailEntity>(request);
                entity.CourtBenchId = request.BenchId != null ? request.BenchId.Value : request.CourtId.Value;
                if (request.CourtDistrictId == Guid.Empty)
                    entity.CourtDistrictId = null;
                if (request.ComplexId == Guid.Empty)
                    entity.ComplexId = null;

                var isAdd = request.AgainstCaseDetails.Where(s => s.CaseNo != null);
                if (isAdd.Count() > 0)
                {
                    var agcs = new List<CaseDetailAgainstEntity>();
                    foreach (var item in request.AgainstCaseDetails)
                    {
                        var ac = new CaseDetailAgainstEntity();
                        ac.ImpugedOrderDate = item.ImpugedOrderDate.Value;
                        ac.CourtTypeId = item.CourtTypeId.Value;
                        ac.CourtBenchId = item.CourtId != null ? item.CourtId.Value : item.BenchId.Value;
                        ac.StateId = item.StateId.Value;
                        ac.CaseYear = item.CaseYear.Value;
                        ac.CaseNo = item.CaseNo;
                        ac.CaseCategoryId = item.CaseCategoryId.Value;
                        ac.CaseTypeId = item.CaseTypeId.Value;
                        ac.StrengthId = item.StrengthId != null ? item.StrengthId.Value : 0;
                        ac.OfficerName = item.OfficerName;
                        ac.CisYear = item.CisYear!=null ?item.CisYear.Value:0;
                        ac.CisNo = item.CisNo;
                        ac.CadreId = item.CadreId;
                        ac.CnrNo = item.CnrNo;
                        ac.CourtDistrictId = item.CourtDistrictId != Guid.Empty ? item.CourtDistrictId : null;
                        ac.ComplexId = item.ComplexId != Guid.Empty ? item.ComplexId : null;
                        agcs.Add(ac);
                    }
                    entity.CaseAgainstEntities = agcs;

                }
                var result = await _Repository.InsertAsync(entity);
                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(entity.Id);
            }
            else
                return Result<Guid>.Fail($"Case number and case year already exist.");
        }


    }
}
