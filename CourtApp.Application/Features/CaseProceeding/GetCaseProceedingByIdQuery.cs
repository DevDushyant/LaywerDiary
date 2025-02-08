using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.DTOs.CaseProceedings;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace CourtApp.Application.Features.CaseProceeding
{
    public class GetCaseProceedingByIdQuery : IRequest<Result<CaseProceedingDto>>
    {
        public Guid CaseId { get; set; }
        public DateTime SelectedDate { get; set; }
    }
    public class GetCaseProceedingByIdQueryHandler : IRequestHandler<GetCaseProceedingByIdQuery, Result<CaseProceedingDto>>
    {
        private readonly ICaseProceedingRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserCaseRepository userCaseRepository;
        public GetCaseProceedingByIdQueryHandler(ICaseProceedingRepository _repository,
            IMapper _mapper, IUserCaseRepository userCaseRepository)
        {
            this._repository = _repository;
            this._mapper = _mapper;
            this.userCaseRepository = userCaseRepository;
        }

        public async Task<Result<CaseProceedingDto>> Handle(GetCaseProceedingByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetByIdAsync(request.CaseId, request.SelectedDate);
            var cd = await userCaseRepository.GetDetailAsync(request.CaseId);
            CaseProceedingDto md = new CaseProceedingDto();
            if (data != null) md = _mapper.Map<CaseProceedingDto>(data);
            md.Court = cd?.CourtBench?.CourtBench_En ?? "";
            md.FirstTitle = cd?.FirstTitle ?? "";
            md.SecondTitle = cd?.SecondTitle ?? "";
            md.Year = cd?.CaseYear != null ? cd.CaseYear.ToString() : "";
            md.No = cd?.CaseNo?.ToString() ?? "";
            md.CaseType = cd?.CaseType?.Name_En ?? "";
            //md.Court = cd != null && cd.CourtBench != null ? cd.CourtBench.CourtBench_En : "";
            //md.FirstTitle = cd != null && cd.CourtBench != null ? cd.FirstTitle : "";
            //md.SecondTitle = cd != null && cd.CourtBench != null ? cd.SecondTitle : "";
            //md.Year = cd != null && cd.CourtBench != null ? cd.CaseYear.ToString() : "";
            //md.No = cd != null && cd.CourtBench != null ? cd.CaseNo.ToString() : "";
            //md.CaseType = cd != null && cd.CourtBench != null ? cd.CaseType.Name_En : "";
            return Result<CaseProceedingDto>.Success(md);
        }
    }

}
