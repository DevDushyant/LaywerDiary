using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.CacheRepositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtFeeStructure.Queries
{
    public class GetCourtFeeStructByIdQuery : IRequest<Result<GetCourtFeeStructureByIdResponse>>
    {
        public Guid Id { get; set; }
    }
    public class GetCourtFeeStructByIdQueryHandler : IRequestHandler<GetCourtFeeStructByIdQuery, Result<GetCourtFeeStructureByIdResponse>>
    {
        private readonly ICourtFeeStructureCacheRepository _repository;
        private readonly IMapper mapper;
        public GetCourtFeeStructByIdQueryHandler(ICourtFeeStructureCacheRepository _repository, IMapper mapper)
        {
            this.mapper = mapper;
            this._repository = _repository;
        }
        public async Task<Result<GetCourtFeeStructureByIdResponse>> Handle(GetCourtFeeStructByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetCacheDataByIdAsync(request.Id);
            var mappeddata = mapper.Map<GetCourtFeeStructureByIdResponse>(data);
            return Result<GetCourtFeeStructureByIdResponse>.Success(mappeddata);
        }
        
    }
}
