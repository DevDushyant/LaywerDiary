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
    public class GetWorkMasterSubCommand : IRequest<Result<List<GetWorkMasterSubResponse>>>
    {
        public Guid Id { get; set; }
    }
    public class GetWorkMasterSubCommandHandler : IRequestHandler<GetWorkMasterSubCommand, Result<List<GetWorkMasterSubResponse>>>
    {
        private readonly IWorkMasterSubRepository repository;
        private readonly IMapper mapper;
        public GetWorkMasterSubCommandHandler(IWorkMasterSubRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Result<List<GetWorkMasterSubResponse>>> Handle(GetWorkMasterSubCommand request, CancellationToken cancellationToken)
        {
            var Heads = await repository.GetListAsync();
            var HeadsDt = mapper.Map<List<GetWorkMasterSubResponse>>(Heads);
            return Result<List<GetWorkMasterSubResponse>>.Success(HeadsDt);

        }
    }
}
