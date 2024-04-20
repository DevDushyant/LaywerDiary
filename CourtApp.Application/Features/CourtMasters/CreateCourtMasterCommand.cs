using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.DTOs.CourtMaster;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtMasters.Command
{
    public class CreateCourtMasterCommand : IRequest<Result<Guid>>
    {
        public Guid CourtTypeId { get; set; }
        public string CourtName { get; set; }
        public int DistrictCode { get; set; }
        public int StateCode { get; set; }
        public Guid CourtDistrictId { get; set; }
        public Guid CourtComplexId { get; set; }
        public List<CourtBenchResponse> CourtBenches { get; set; }
    }

    public class CreateCourtMasterCommandHandler : IRequestHandler<CreateCourtMasterCommand, Result<Guid>>
    {
        private readonly ICourtMasterRepository repository;
        private readonly ICourtTypeCacheRepository _CourtTypeRepo;
        private readonly IStateMasterRepository _StateRepo;
        private readonly IDistrictMasterRepository _DistrictRepo;
        private readonly ICourtBenchRepository _CourtBenchRepo;
        private readonly IMapper mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public CreateCourtMasterCommandHandler(ICourtMasterRepository repository, IMapper mapper,
            IUnitOfWork _unitOfWork, ICourtTypeCacheRepository _CourtTypeRepo,
            IStateMasterRepository _StateRepo, IDistrictMasterRepository districtRepo, ICourtBenchRepository courtBenchRepository)
        {
            this.repository = repository;
            this.mapper = mapper;
            this._unitOfWork = _unitOfWork;
            this._CourtTypeRepo = _CourtTypeRepo;
            this._StateRepo = _StateRepo;
            _DistrictRepo = districtRepo;
            this._CourtBenchRepo = courtBenchRepository;
        }
        public async Task<Result<Guid>> Handle(CreateCourtMasterCommand request, CancellationToken cancellationToken)
        {
            var mappeddata = mapper.Map<CourtMasterEntity>(request);
            mappeddata.CourtType = _CourtTypeRepo.GetByIdAsync(request.CourtTypeId).Result;
            mappeddata.State = _StateRepo.GetStateById(request.StateCode);
            mappeddata.District = _DistrictRepo.GetDistrictById(request.DistrictCode);
            await repository.InsertAsync(mappeddata);
            await _unitOfWork.Commit(cancellationToken);
            if (mappeddata.Id != Guid.Empty)
            {
                List<CourtBenchEntity> courtBenchEntities = new List<CourtBenchEntity>();
                Guid CourtId = Guid.Empty;
                if (mappeddata.CourtTypeId == Guid.Parse("359acb34-7b8c-4fc8-a276-8b198ea5105c"))
                    CourtId = mappeddata.Id;
                else
                    CourtId = mappeddata.CourtComplexId.Value;
                foreach (var item in request.CourtBenches)
                {
                    courtBenchEntities.Add(new CourtBenchEntity
                    {
                        Id = Guid.NewGuid(),
                        CourtBench_En = item.CourtBench_En,
                        CourtMasterId = CourtId,
                        Address = item.Address,
                    });
                }
                await _CourtBenchRepo.InsertAsync(courtBenchEntities);
            }
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(mappeddata.Id);
        }
    }
}
