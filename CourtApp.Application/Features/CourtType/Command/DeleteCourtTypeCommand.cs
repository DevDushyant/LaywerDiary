using AspNetCoreHero.Results;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtType.Command
{
    public class DeleteCourtTypeCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteCourtTypeCommandHandler : IRequestHandler<DeleteCourtTypeCommand, Result<Guid>>
    {
        private readonly ICourtTypeRepository _Repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCourtTypeCommandHandler(ICourtTypeRepository _Repository, IUnitOfWork unitOfWork)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(DeleteCourtTypeCommand request, CancellationToken cancellationToken)
        {
            var courtType = await _Repository.GetByIdAsync(request.Id);
            if (courtType == null)
                return Result<Guid>.Fail($"Court Type Not Found.");

            await _Repository.DeleteAsync(courtType);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(courtType.Id);
        }
    }
}
