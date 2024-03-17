using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.ProceedingHead
{
    public class ProceedingHeadGetByIdCommand : IRequest<Result<GetProceedingHeadResponse>>
    {
        
    }
    public class ProceedingHeadGetByIdCommandHandler : IRequestHandler<ProceedingHeadGetByIdCommand, Result<GetProceedingHeadResponse>>
    {
        private readonly IProceedingHeadRepository repository;
        private readonly IMapper mapper;
        public ProceedingHeadGetByIdCommandHandler(IProceedingHeadRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Result<GetProceedingHeadResponse>> Handle(ProceedingHeadGetByIdCommand request, CancellationToken cancellationToken)
        {
            var Heads = await repository.GetListAsync();
            var HeadsDt = mapper.Map<GetProceedingHeadResponse>(Heads);
            return Result<GetProceedingHeadResponse>.Success(HeadsDt);

        }
    }


}
