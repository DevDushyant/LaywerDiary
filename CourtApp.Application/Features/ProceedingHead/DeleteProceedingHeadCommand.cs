using AspNetCoreHero.Results;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.ProceedingHead
{
    public class DeleteProceedingHeadCommand:IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteProceedingHeadCommandHandler : IRequestHandler<DeleteProceedingHeadCommand, Result<Guid>>
    {
        private readonly IProceedingHeadRepository repository;
        private IUnitOfWork _unitOfWork { get; set; }
        public DeleteProceedingHeadCommandHandler(IUnitOfWork unitOfWork, IProceedingHeadRepository repository)
        {

            _unitOfWork = unitOfWork;
            this.repository = repository;
        }
        public async Task<Result<Guid>> Handle(DeleteProceedingHeadCommand request, CancellationToken cancellationToken)
        {
            var detail = await repository.GetByIdAsync(request.Id);
            await repository.DeleteAsync(detail);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(detail.Id);
        }
    }
}
