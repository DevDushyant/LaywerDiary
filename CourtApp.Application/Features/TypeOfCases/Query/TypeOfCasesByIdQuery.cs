using CourtApp.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Application.Interfaces.Repositories;

namespace CourtApp.Application.Features.Typeofcasess.Query
{
    public class TypeOfCasesByIdQuery : IRequest<Result<TypeOfCasesQueryByIdResponse>>
    {
        public int Id { get; set; }
        public TypeOfCasesByIdQuery()
        {

        }
    }

    public class CaseKindByIdQueryCommandHandler : IRequestHandler<TypeOfCasesByIdQuery, Result<TypeOfCasesQueryByIdResponse>>
    {
        private readonly ITypeOfCasesRepository _repository;
        private readonly IMapper mapper;
        public CaseKindByIdQueryCommandHandler(ITypeOfCasesRepository _repository, IMapper _mapper)
        {
            this._repository = _repository;
            this.mapper = _mapper;
        }
        public async Task<Result<TypeOfCasesQueryByIdResponse>> Handle(TypeOfCasesByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetByIdAsync(request.Id);
            var mappeddata = mapper.Map<TypeOfCasesQueryByIdResponse>(data);
            return Result<TypeOfCasesQueryByIdResponse>.Success(mappeddata);
        }
    }
}
