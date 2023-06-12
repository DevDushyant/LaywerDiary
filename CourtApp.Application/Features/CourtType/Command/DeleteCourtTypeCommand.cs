using AspNetCoreHero.Results;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtType.Command
{
    public class DeleteCourtTypeCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteCourtTypeCommandHandler : IRequestHandler<DeleteCourtTypeCommand, Result<int>>
    {
        private readonly ICourtTypeRepository _Repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCourtTypeCommandHandler(ICourtTypeRepository _Repository, IUnitOfWork unitOfWork)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(DeleteCourtTypeCommand request, CancellationToken cancellationToken)
        {
            var courtType = await _Repository.GetByIdAsync(request.Id);
            if (courtType == null)
                return Result<int>.Fail($"Court Type Not Found.");

            await _Repository.DeleteAsync(courtType);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(courtType.Id);
        }
    }
}
