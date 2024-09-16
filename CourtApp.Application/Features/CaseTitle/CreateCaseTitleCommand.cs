using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseTitle
{
    public class CreateCaseTitleCommand : IRequest<Result<Guid>>
    {
        public Guid CaseId { get; set; }
        public int TypeId { get; set; }
        public List<CaseApplicantDetail> CaseApplicants { get; set; }
    }
    
    public class CreateCaseTitleCommandHandler : IRequestHandler<CreateCaseTitleCommand, Result<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IUserCaseRepository _UserCaseRepo;
        private readonly ICaseTitleRepository _CaseTitleRepository;
        private IUnitOfWork _unitOfWork { get; set; }
        public CreateCaseTitleCommandHandler(IUserCaseRepository _UserCaseRepo,
            IMapper _mapper, IUnitOfWork unitOfWork, ICaseTitleRepository caseTitleRepository)
        {
            this._mapper = _mapper;
            this._UserCaseRepo = _UserCaseRepo;
            _unitOfWork = unitOfWork;
            _CaseTitleRepository = caseTitleRepository;

        }
        public async Task<Result<Guid>> Handle(CreateCaseTitleCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CaseTitleEntity>(request);
            entity.CaseApplicants = _mapper.Map<List<CaseApplicantDetailEntity>>(request.CaseApplicants);
            await _CaseTitleRepository.InsertAsync(entity);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(entity.Id);
        }
    }
}
