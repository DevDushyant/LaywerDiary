using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static CourtApp.Application.Constants.Permissions;
namespace CourtApp.Application.Features.CaseTitle
{
    public class UpdateCaseTitleCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public Guid CaseId { get; set; }
        public int TypeId { get; set; }
        public List<CaseApplicantDetail> CaseApplicants { get; set; }
    }
    public class UpdateCaseTitleCommandHandler : IRequestHandler<UpdateCaseTitleCommand, Result<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IUserCaseRepository _UserCaseRepo;
        private readonly ICaseTitleRepository _CaseTitleRepository;
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateCaseTitleCommandHandler(IUserCaseRepository _UserCaseRepo,
            IMapper _mapper, IUnitOfWork unitOfWork, ICaseTitleRepository caseTitleRepository)
        {
            this._mapper = _mapper;
            this._UserCaseRepo = _UserCaseRepo;
            _unitOfWork = unitOfWork;
            _CaseTitleRepository = caseTitleRepository;

        }
        public async Task<Result<Guid>> Handle(UpdateCaseTitleCommand request, CancellationToken cancellationToken)
        {
            var TitleDetail = await _CaseTitleRepository.GetByIdAsync(request.Id);
            if (TitleDetail == null)
                return Result<Guid>.Fail($"Case title not found.");
            else
            {
                TitleDetail.CaseId = request.CaseId;
                TitleDetail.TypeId = request.TypeId;
                TitleDetail.CaseApplicants = _mapper.Map<List<CaseApplicantDetailEntity>>(request.CaseApplicants);
                await _CaseTitleRepository.UpdateAsync(TitleDetail);
                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(TitleDetail.Id);
            }
        }
    }
}
