using CourtApp.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseNatures.Command
{
    public class DeleteCaseNatureCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteCaseNatureCommandHandler : IRequestHandler<DeleteCaseNatureCommand, Result<int>>
    {
        private readonly ICaseNatureRepository _Repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCaseNatureCommandHandler(ICaseNatureRepository _Repository, IUnitOfWork unitOfWork)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteCaseNatureCommand command, CancellationToken cancellationToken)
        {
            var detail = await _Repository.GetByIdAsync(command.Id);
            await _Repository.DeleteAsync(detail);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(detail.Id);
        }
    }
}
