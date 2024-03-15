using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.CacheRepositories;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtMasters.Command
{
    public class CreateCourtMasterCommand : IRequest<Result<Guid>>
    {
        public Guid CourtTypeId { get; set; }
        public string CourtName { get; set; }
        public string CourtFullName { get; set; }
        public string Bench { get; set; }
        public string HeadQuerter { get; set; }
        public string Address { get; set; }
        public int DistrictCode { get; set; }
        public int StateCode { get; set; }        
    }

    public class CreateCourtMasterCommandHandler : IRequestHandler<CreateCourtMasterCommand, Result<Guid>>
    {
        private readonly ICourtMasterRepository repository;
        private readonly ICourtTypeCacheRepository _CourtTypeRepo;
        private readonly IStateMasterRepository _StateRepo;
        private readonly IDsitrictMasterRepository _DistrictRepo;
        private readonly IMapper mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public CreateCourtMasterCommandHandler(ICourtMasterRepository repository, IMapper mapper,
            IUnitOfWork _unitOfWork, ICourtTypeCacheRepository _CourtTypeRepo,
            IStateMasterRepository _StateRepo, IDsitrictMasterRepository districtRepo)
        {
            this.repository = repository;
            this.mapper = mapper;
            this._unitOfWork = _unitOfWork;
            this._CourtTypeRepo = _CourtTypeRepo;
            this._StateRepo = _StateRepo;
            _DistrictRepo = districtRepo;
        }
        public async Task<Result<Guid>> Handle(CreateCourtMasterCommand request, CancellationToken cancellationToken)
        {
            var mappeddata = mapper.Map<CourtMasterEntity>(request);
            mappeddata.CourtType = _CourtTypeRepo.GetByIdAsync(request.CourtTypeId).Result;
            mappeddata.State = _StateRepo.GetStateById(request.StateCode);
            mappeddata.District = _DistrictRepo.GetDistrictById(request.DistrictCode);
            await repository.InsertAsync(mappeddata);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(mappeddata.Id);
        }
    }
}
