using CourtApp.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace CourtApp.Application.Features.CaseNatures.Query
{
    public class CaseNatureByIdQuery : IRequest<Result<CaseNatureByIdResponse>>
    {
        public Guid Id { get; set; }
        public CaseNatureByIdQuery()
        {

        }
    }

    public class CaseNatureByIdQueryCommandHandler : IRequestHandler<CaseNatureByIdQuery, Result<CaseNatureByIdResponse>>
    {
        private readonly ICaseNatureCacheRepository _repository;
        private readonly IMapper mapper;
        public CaseNatureByIdQueryCommandHandler(ICaseNatureCacheRepository _repository, IMapper _mapper)
        {
            this._repository = _repository;
            this.mapper = _mapper;
        }
        public async Task<Result<CaseNatureByIdResponse>> Handle(CaseNatureByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetByIdAsync(request.Id);
            var mappeddata = mapper.Map<CaseNatureByIdResponse>(data);
            return Result<CaseNatureByIdResponse>.Success(mappeddata);
        }
    }
}
