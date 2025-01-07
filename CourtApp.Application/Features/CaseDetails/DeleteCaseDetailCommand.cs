using AspNetCoreHero.Results;
using CourtApp.Application.Features.BookTypes.Command;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails
{
    public class DeleteCaseDetailCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteCaseDetailCommandHandler : IRequestHandler<DeleteCaseDetailCommand, Result<Guid>>
    {
        private readonly IUserCaseRepository _Repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCaseDetailCommandHandler(IUserCaseRepository _Repository, IUnitOfWork unitOfWork)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteCaseDetailCommand command, CancellationToken cancellationToken)
        {
            var detail = await _Repository.GetByIdAsync(command.Id);
            if (detail == null) return Result<Guid>.Fail("Record is not found for deletion!");
            await _Repository.DeleteAsync(detail);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(detail.Id);
        }
    }
}
