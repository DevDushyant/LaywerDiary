using System;
using CourtApp.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseKinds.Commands
{
   public class DeleteCaseKindCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public DeleteCaseKindCommand()
        {

        }
        
    }

    public class DeleteCaseKindCommandHandler : IRequestHandler<DeleteCaseKindCommand, Result<int>>
    {
        private readonly ICaseKindRepository _Repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCaseKindCommandHandler(ICaseKindRepository _Repository, IUnitOfWork unitOfWork)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteCaseKindCommand command, CancellationToken cancellationToken)
        {
            var detail = await _Repository.GetByIdAsync(command.Id);
            await _Repository.DeleteAsync(detail);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(detail.Id);
        }
    }
}
