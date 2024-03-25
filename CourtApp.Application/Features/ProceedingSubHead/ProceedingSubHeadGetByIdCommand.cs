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
    public class ProceedingSubHeadGetByIdCommand:IRequest<Result<GetProceedingSubHeadResponse>>
    {
    }
    public class ProceedingSubHeadGetByIdCommandHandler : IRequestHandler<ProceedingSubHeadGetByIdCommand, Result<GetProceedingSubHeadResponse>>
    {
        private readonly IProceedingSubHeadRepository repository;
        private readonly IMapper mapper;
        public ProceedingSubHeadGetByIdCommandHandler(IProceedingSubHeadRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Result<GetProceedingSubHeadResponse>> Handle(ProceedingSubHeadGetByIdCommand request, CancellationToken cancellationToken)
        {
            var Heads = await repository.GetListAsync();
            var HeadsDt = mapper.Map<GetProceedingSubHeadResponse>(Heads);
            return Result<GetProceedingSubHeadResponse>.Success(HeadsDt);

        }
    }
}
