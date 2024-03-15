using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.ProceedingHead
{
    public class GetProceedingHeadCommand : IRequest<Result<List<GetProceedingHeadResponse>>>
    {
        
    }
    public class GetProceedingHeadCommandHandler : IRequestHandler<GetProceedingHeadCommand, Result<List<GetProceedingHeadResponse>>>
    {
        private readonly IProceedingHeadRepository repository;
        private readonly IMapper mapper;
        public GetProceedingHeadCommandHandler(IProceedingHeadRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Result<List<GetProceedingHeadResponse>>> Handle(GetProceedingHeadCommand request, CancellationToken cancellationToken)
        {
            var Heads = await repository.GetListAsync();
            var HeadsDt = mapper.Map<List<GetProceedingHeadResponse>>(Heads);
            return Result<List<GetProceedingHeadResponse>>.Success(HeadsDt);
        }
    }


}
