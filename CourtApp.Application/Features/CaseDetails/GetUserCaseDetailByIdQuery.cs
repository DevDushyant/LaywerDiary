using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.DTOs.CaseDetails;
using CourtApp.Application.Features.CaseKinds.Query;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.UserCase
{
    public class GetUserCaseDetailByIdQuery : IRequest<Result<UserCaseDetailResponse>>
    {
        public Guid CaseId { get; set; }
    }

    public class GetUserCaseDetailByIdQueryHandler : IRequestHandler<GetUserCaseDetailByIdQuery, Result<UserCaseDetailResponse>>
    {
        private readonly IUserCaseRepository _CaseRepo;
        private readonly IMapper _mapper;
        public GetUserCaseDetailByIdQueryHandler(IUserCaseRepository _CaseRepo, IMapper _mapper)
        {
            this._CaseRepo = _CaseRepo;
            this._mapper = _mapper; 
        }
        public async Task<Result<UserCaseDetailResponse>> Handle(GetUserCaseDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var detail = await _CaseRepo.GetByIdAsync(request.CaseId);
            var mappeddata = _mapper.Map<UserCaseDetailResponse>(detail);
            mappeddata.CaseCategory = detail.CaseCategory.Name_En;
            mappeddata.CourtBench = detail.CourtBench.CourtBench_En;
            mappeddata.CourtType = detail.CourtType.CourtType;
            mappeddata.CaseType = detail.CaseType.Name_En;
            return Result<UserCaseDetailResponse>.Success(mappeddata);
        }
    }
}
