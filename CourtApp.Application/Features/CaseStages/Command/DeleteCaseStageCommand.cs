using System;
using CourtApp.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseStages.Command
{
    public class DeleteCaseStageCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public DeleteCaseStageCommand()
        {

        }
    }

    public class DeleteCaseStageCommandHandler : IRequestHandler<DeleteCaseStageCommand, Result<int>>
    {
        private readonly ICaseStageRepository _Repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCaseStageCommandHandler(ICaseStageRepository _Repository, IUnitOfWork unitOfWork)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(DeleteCaseStageCommand request, CancellationToken cancellationToken)
        {
            var detail = await _Repository.GetByIdAsync(request.Id);
            await _Repository.DeleteAsync(detail);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(detail.Id);
        }
    }
}
