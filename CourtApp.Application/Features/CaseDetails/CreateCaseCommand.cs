using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Case
{
    public class CreateCaseCommand : IRequest<Result<Guid>>
    {
        public DateTime InstitutionDate { get; set; }
        public Guid CourtTypeId { get; set; }
        public Guid CaseCategoryId { get; set; }
        public Guid CaseTypeId { get; set; }
        public Guid CourtBenchId { get; set; }
        public string CaseNo { get; set; }
        public int CaseYear { get; set; }
        public string FirstTitle { get; set; }
        public int FirstTitleCode { get; set; }
        public string SecondTitle { get; set; }
        public int SecoundTitleCode { get; set; }
        public string CisNumber { get; set; }
        public int CisYear { get; set; }
        public string CnrNumber { get; set; }
        public DateTime? NextDate { get; set; }
        public string CaseStageCode { get; set; }
        public Guid LinkedCaseId { get; set; }
        public Guid ClientId { get; set; }
        public ICollection<CaseAgainstEntityModel> AgainstCaseDetails { get; set; }
    }
    public class CaseAgainstEntityModel
    {
        public Guid Id { get; set; }
        public DateTime ImpugedOrderDate { get; set; }
        public Guid CourtTypeId { get; set; }
        public Guid CourtBenchId { get; set; }
        public Guid CaseCategoryId { get; set; }
        public Guid CaseTypeId { get; set; }
        public int StateId { get; set; }
        public int? StrengthId { get; set; }
        public string CaseNo { get; set; }
        public int CaseYear { get; set; }
        public int CisYear { get; set; }       
        public string OfficerName { get; set; }
        public string Cadre { get; set; }
        public Guid? CourtDistrictId { get; set; }
        public Guid? AgainstBenchId { get; set; }
        public Guid? ComplexId { get; set; }
        public Guid? CourtId { get; set; }
        public string CisNumber { get; set; }
        public string CnrNumber { get; set; }
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
            var entity = _mapper.Map<CaseDetailEntity>(request);
            entity.CaseAgainstEntities= _mapper.Map<List<CaseDetailAgainstEntity>>(request.AgainstCaseDetails);
            var result = await _Repository.InsertAsync(entity);
            await _unitOfWork.Commit(cancellationToken);
            //if (entity.Id != Guid.Empty)
            //{
            //    List<CaseDetailAgainstEntity> againstEntities = new List<CaseDetailAgainstEntity>();
            //    foreach (var item in request.AgainstCaseDetails)
            //    {
            //        againstEntities.Add(new CaseDetailAgainstEntity
            //        {
            //            Id=Guid.NewGuid(),
            //            CaseId = entity.Id,
            //            Cadre = item.Cadre,                        
            //            CaseNo = item.CaseNo,
            //            CaseYear = item.CaseYear,
            //            CisNo = item.CisNo,
            //            CisYear = item.CisYear,
            //            CnrNo = item.CnrNo,
            //            CourtBenchId = item.CourtBenchId,
            //            CourtTypeId = item.CourtTypeId,
            //            ImpugedOrderDate = item.ImpugedOrderDate,
            //            OfficerName = item.OfficerName,
            //            StateId = item.StateId,
            //            StrengthId = item.StrengthId
            //        });
            //    }
            //    await _CaseAgainstRepo.InsertAsync(againstEntities);
            //    await _unitOfWork.Commit(cancellationToken);
            //}            
            return Result<Guid>.Success(entity.Id);
        }
    }
}
