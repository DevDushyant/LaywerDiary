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

namespace CourtApp.Application.Features.WorkMasterSub
{
    public class WorkMasterSubGetByIdCommand : IRequest<Result<GetWorkMasterSubResponse>>
    {
    }
    public class WorkMasterSubGetByIdCommandHandler : IRequestHandler<WorkMasterSubGetByIdCommand, Result<GetWorkMasterSubResponse>>
    {
        private readonly IWorkMasterSubRepository repository;
        private readonly IMapper mapper;
        public WorkMasterSubGetByIdCommandHandler(IWorkMasterSubRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Result<GetWorkMasterSubResponse>> Handle(WorkMasterSubGetByIdCommand request, CancellationToken cancellationToken)
        {
            var Heads = await repository.GetListAsync();
            var HeadsDt = mapper.Map<GetWorkMasterSubResponse>(Heads);
            return Result<GetWorkMasterSubResponse>.Success(HeadsDt);

        }
    }
}
