using AspNetCoreHero.Results;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMasterSub
{
    public class UpdateWorkSubMstCommand:IRequest<Result<Guid>>
    {
        public Guid Id{ get; set; }
        public Guid WorkId { get; set; }
        public required string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
    }
    public class UpdateWorkSubMstCommandHandler : IRequestHandler<UpdateWorkSubMstCommand, Result<Guid>>
    {
        private readonly IWorkMasterSubRepository repository;
        private readonly IUnitOfWork unitOfWork;
        public UpdateWorkSubMstCommandHandler(IWorkMasterSubRepository repository, IUnitOfWork unitOfWork)
        {            
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(UpdateWorkSubMstCommand request, CancellationToken cancellationToken)
        {
            var Detail = await repository.GetByIdAsync(request.Id);
            if (Detail == null)
                return Result<Guid>.Fail($"Sub master ");
            else
            {
                Detail.Name_En = request.Name_En;
                Detail.Name_Hn = request.Name_Hn;
                Detail.Abbreviation = request.Abbreviation;
                Detail.WorkId= request.WorkId;
                await repository.UpdateAsync(Detail);
                await unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(Detail.Id);
            }
        }
    }
}
