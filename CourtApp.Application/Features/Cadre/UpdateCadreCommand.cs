using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Cadre
{
    public class UpdateCadreCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
    public class UpdateCadreCommandHandler : IRequestHandler<UpdateCadreCommand, Result<Guid>>
    {
        private readonly ICadreMasterRepository repository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public UpdateCadreCommandHandler(ICadreMasterRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(UpdateCadreCommand request, CancellationToken cancellationToken)
        {
            var detail = await repository.GetByIdAsync(request.Id);
            if (detail == null) return Result<Guid>.Fail("Record is not exist");
            else
            {
                detail.Name_En = request.Name_En;
                detail.Name_Hn = request.Name_Hn;
                await repository.UpdateAsync(detail);
                await unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(detail.Id);
            }
        }
    }
}
