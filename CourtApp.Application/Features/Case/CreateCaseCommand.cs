using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Constants;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Case
{
    public class CreateCaseCommand : IRequest<Result<Guid>>
    {
        public required DateTime InstitutionDate { get; set; }
        public required Guid NatureId { get; set; }
        public required Guid CaseTypeId { get; set; }
        public required Guid CourtTypeId { get; set; }
        public required Guid CourtId { get; set; }
        public required Guid CaseKindId { get; set; }
        public required string Number { get; set; }
        public required int Year { get; set; }
        public string CisNumber { get; set; }
        public int CisYear { get; set; }
        public string CnrNumber { get; set; }
        public required string FirstTitle { get; set; }
        public required int FirstTitleCode { get; set; }
        public required string SecondTitle { get; set; }
        public required int SecoundTitleCode { get; set; }
        public DateTime NextDate { get; set; }
        public string CaseStageCode { get; set; }
        public Guid LinkedCaseId { get; set; }
        public Guid ClientId { get; set; }
        public List<CaseAgainstEntityModel> AgainstCaseDetails { get; set; }
    }
    public class CaseAgainstEntityModel
    {
        public DateTime ImpugedDate { get; set; }
        public Guid CourtTypeId { get; set; }
        public Guid CourtId { get; set; }
        public string Number { get; set; }
        public int CaseYear { get; set; }
        public string CisNumber { get; set; }
        public int CisYear { get; set; }
        public string CnrNumber { get; set; }
        public string ProcOfficer { get; set; }
        public string Cadre { get; set; }
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
        
        private IUnitOfWork _unitOfWork { get; set; }
        public CreateCaseManagmentCommandHandler(IUserCaseRepository _Repository,
            IMapper _mapper, IUnitOfWork _unitOfWork,
            ICaseKindCacheRepository caseKindCacheRepository,
            IClientCacheRepository clientCacheRepository,
            ICaseStageCacheRepository caseStageCacheRepository,
            ICourtTypeCacheRepository _CourtTypeCacheRepository,
            ITypeOfCasesCacheRepository _ITypeOfCasesCacheRepository,
            ICourtMasterCacheRepository _CourtMasterCacheRepository, 
            ICaseNatureCacheRepository _CaseNatureCacheRepository
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
            this._CaseNatureCacheRepository= _CaseNatureCacheRepository;
        }
        public async Task<Result<Guid>> Handle(CreateCaseCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CaseEntity>(request);
            //var against = _mapper.Map<List<AgainstCaseDetails>>(request.AgainstCaseDetails);
            //if (request.LinkedWith != Guid.Empty)
            //    entity.Client = _ClientCacheRepository.GetCachedListAsync().Result.Where(c => c.Id == request.LinkedWith).FirstOrDefault();
            //if (request.NatureId != Guid.Empty)
            //    entity.CaseNature = _CaseNatureCacheRepository.GetCachedListAsync().Result.Where(cn => cn.Id == request.NatureId).FirstOrDefault();
            //if (request.TypeCaseId != Guid.Empty)
            //    entity.TypeOfCase = _ITypeOfCasesCacheRepository.GetCachedListAsync().Result.Where(w => w.Id == request.TypeCaseId).FirstOrDefault();
            //if (request.CourtTypeId != Guid.Empty)
            //    entity.CourtType = _CourtTypeCacheRepository.GetCachedListAsync().Result.Where(w => w.Id == request.CourtTypeId).FirstOrDefault();
            //if (request.CourtId != Guid.Empty)
            //    entity.Court = _CourtMasterCacheRepository.GetCachedListAsync().Result.Where(w => w.Id == request.CourtId).FirstOrDefault();
            //if (request.CaseTypeId != Guid.Empty)
            //    entity.CaseType = _CaseKindCacheRepository.GetCachedListAsync().Result.Where(w => w.Id == request.CaseTypeId).FirstOrDefault();
           
            var result = await _Repository.InsertAsync(entity);
            await _unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(entity.Id);
        }
    }
}
