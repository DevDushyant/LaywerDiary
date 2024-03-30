using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
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
        public int Type { get; set; }
        public List<string> Title { get; set; }
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
            var CaseTitle = _mapper.Map<CaseTitleEntity>(request);
            CaseTitle.Case = _UserCaseRepo.GetByIdAsync(request.CaseId).Result;            
            await _CaseTitleRepository.InsertAsync(CaseTitle);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(CaseTitle.Id);
        }
    }
}
