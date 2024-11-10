using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.DTOs.CaseProceedings;
using CourtApp.Application.Features.CaseKinds.Query;
using MediatR;
using CourtApp.Application.Interfaces.Repositories;


namespace CourtApp.Application.Features.CaseProceeding
{
    public class GetCaseProceedingByIdQuery:IRequest<Result<CaseProceedingDto>>
    {
        public Guid CaseId{ get; set; }
        public DateTime SelectedDate { get; set; }
    }
    public class GetCaseProceedingByIdQueryHandler : IRequestHandler<GetCaseProceedingByIdQuery, Result<CaseProceedingDto>>
    {
        private readonly ICaseProceedingRepository _repository;
        private readonly IMapper _mapper;
        public GetCaseProceedingByIdQueryHandler(ICaseProceedingRepository _repository, IMapper _mapper)
        {
            this._repository = _repository;
            this._mapper=_mapper    ;                            
        }

        public async Task<Result<CaseProceedingDto>> Handle(GetCaseProceedingByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetByIdAsync(request.CaseId,request.SelectedDate);
            var mappeddata = _mapper.Map<CaseProceedingDto>(data);
            return Result<CaseProceedingDto>.Success(mappeddata);
        }       
    }

}
