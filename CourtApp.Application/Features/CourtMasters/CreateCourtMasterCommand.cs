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
        //public int DistrictCode { get; set; }
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
            var entity = mapper.Map<CourtMasterEntity>(request);
            entity.CourtComplexId = request.CourtComplexId != Guid.Empty ? request.CourtComplexId : null;
            entity.CourtDistrictId = request.CourtDistrictId != Guid.Empty ? request.CourtDistrictId : null;
            await repository.InsertAsync(entity);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(entity.Id);
            //if (request.CourtBenches != null && request.CourtBenches.Count > 0)
            //{
            //    foreach (var entity in request.CourtBenches)
            //    {
            //        CourtMasterEntity ent = new CourtMasterEntity();
            //        ent.Name_En = entity.CourtBench_En;
            //        ent.Name_Hn = entity.CourtBench_Hn;
            //        ent.StateId = request.StateCode;
            //        ent.CourtComplexId = request.CourtComplexId != Guid.Empty ? request.CourtComplexId : null;
            //        ent.CourtDistrictId = request.CourtDistrictId != Guid.Empty ? request.CourtDistrictId : null;
            //        ent.CourtTypeId = request.CourtTypeId;
            //        ent.Abbreviation = ent.Abbreviation;
            //        await repository.InsertAsync(ent);
            //        await _unitOfWork.Commit(cancellationToken);
            //        return Result<Guid>.Success(ent.Id);
            //    }                
            //}
            //return Result<Guid>.Fail("Court/Bench information not provided");

        }
    }
}
