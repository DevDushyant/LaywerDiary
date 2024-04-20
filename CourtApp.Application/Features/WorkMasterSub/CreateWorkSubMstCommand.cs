using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMasterSub
{
    public class CreateWorkSubMstCommand:IRequest<Result<Guid>>
    {        
        public Guid WorkId { get; set; }
        public required string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
    }
    public class CreateWorkSubMstCommandHandler : IRequestHandler<CreateWorkSubMstCommand, Result<Guid>>
    {
        private readonly IWorkMasterSubRepository repository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public CreateWorkSubMstCommandHandler(IWorkMasterSubRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.repository = repository;   
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CreateWorkSubMstCommand request, CancellationToken cancellationToken)
        {
            var mappeddata = mapper.Map<WorkMasterSubEntity>(request);
            await repository.InsertAsync(mappeddata);
            await unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(mappeddata.Id);
        }
    }
}
