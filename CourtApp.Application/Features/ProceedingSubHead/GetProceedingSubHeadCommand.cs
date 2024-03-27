using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Features.ProceedingHead;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.ProceedingSubHead
{
    public class GetProceedingSubHeadCommand : IRequest<Result<List<GetProceedingSubHeadResponse>>>
    {
        public Guid Id { get; set; }
    }
    public class GetProceedingSubHeadCommandHandler : IRequestHandler<GetProceedingSubHeadCommand, Result<List<GetProceedingSubHeadResponse>>>
    {
        private readonly IProceedingSubHeadRepository repository;
        private readonly IMapper mapper;
        public GetProceedingSubHeadCommandHandler(IProceedingSubHeadRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Result<List<GetProceedingSubHeadResponse>>> Handle(GetProceedingSubHeadCommand request, CancellationToken cancellationToken)
        {
            var Heads = await repository.GetListAsync();
            var HeadsDt = mapper.Map<List<GetProceedingSubHeadResponse>>(Heads);
            return Result<List<GetProceedingSubHeadResponse>>.Success(HeadsDt);

        }
    }
}
