using CourtApp.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace CourtApp.Application.Features.CaseNatures.Command
{
    public class DeleteCaseNatureCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteCaseNatureCommandHandler : IRequestHandler<DeleteCaseNatureCommand, Result<Guid>>
    {
        private readonly ICaseNatureRepository _Repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCaseNatureCommandHandler(ICaseNatureRepository _Repository, IUnitOfWork unitOfWork)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteCaseNatureCommand command, CancellationToken cancellationToken)
        {
            var detail = await _Repository.GetByIdAsync(command.Id);
            await _Repository.DeleteAsync(detail);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(detail.Id);
        }
    }
}
