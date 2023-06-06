using System;
using CourtApp.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CourtApp.Application.Features.Typeofcasess.Query
{
    public class TypeOfCasesAllCacheQuery : IRequest<Result<List<TypeOfCasesCacheQueryResponse>>>
    {
        public TypeOfCasesAllCacheQuery()
        {

        }
    }

    public class CaseKindAllCacheQueryCommandHandler : IRequestHandler<TypeOfCasesAllCacheQuery, Result<List<TypeOfCasesCacheQueryResponse>>>
    {
        private readonly ITypeOfCasesCacheRepository _repository;
        private readonly IMapper _mapper;

        public CaseKindAllCacheQueryCommandHandler(ITypeOfCasesCacheRepository _repository, IMapper _mapper)
        {
            this._repository = _repository;
            this._mapper = _mapper;
        }
        public async Task<Result<List<TypeOfCasesCacheQueryResponse>>> Handle(TypeOfCasesAllCacheQuery request, CancellationToken cancellationToken)
        {
            var dt = await _repository.GetCachedListAsync();
            var mappeddata = _mapper.Map<List<TypeOfCasesCacheQueryResponse>>(dt);
            return Result<List<TypeOfCasesCacheQueryResponse>>.Success(mappeddata);
        }
    }
}
