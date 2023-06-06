using AspNetCoreHero.Results;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtFeeStructure.Command
{
    public class DeleteCourtFeeStructureCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }       
    }

    public class DeleteCourtFeeStructureCommandHandler : IRequestHandler<DeleteCourtFeeStructureCommand, Result<Guid>>
    {
        private readonly ICourtFeeStructureRepository repository;
        private IUnitOfWork _unitOfWork { get; set; }
        public DeleteCourtFeeStructureCommandHandler(ICourtFeeStructureRepository repository, IUnitOfWork _unitOfWork)
        {
            this.repository = repository;
            this._unitOfWork = _unitOfWork;

        }
        public async Task<Result<Guid>> Handle(DeleteCourtFeeStructureCommand request, CancellationToken cancellationToken)
        {
            var detail = await repository.GetByIdAsync(request.Id);
            if (detail == null)
                return Result<Guid>.Fail($"Fee structure detail Not Found.");
            else
            {
                await repository.DeleteAsync(detail);
                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(detail.Id);
            }
        }
    }
}
