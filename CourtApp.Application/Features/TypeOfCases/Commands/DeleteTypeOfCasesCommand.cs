using System;
using CourtApp.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Typeofcasess.Commands
{
   public class DeleteTypeOfCasesCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public DeleteTypeOfCasesCommand()
        {

        }
        
    }

    public class DeleteCaseKindCommandHandler : IRequestHandler<DeleteTypeOfCasesCommand, Result<int>>
    {
        private readonly ITypeOfCasesRepository _Repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCaseKindCommandHandler(ITypeOfCasesRepository _Repository, IUnitOfWork unitOfWork)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteTypeOfCasesCommand command, CancellationToken cancellationToken)
        {
            var detail = await _Repository.GetByIdAsync(command.Id);
            await _Repository.DeleteAsync(detail);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(detail.Id);
        }
    }
}
