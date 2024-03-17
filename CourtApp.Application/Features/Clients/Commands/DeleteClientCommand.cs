using CourtApp.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace CourtApp.Application.Features.Clients.Commands
{
    public class DeleteClientCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }        
    }
    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Result<Guid>>
    {
        private readonly IClientRepository _Repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteClientCommandHandler(IClientRepository _Repository, IUnitOfWork unitOfWork)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteClientCommand command, CancellationToken cancellationToken)
        {
            var client = await _Repository.GetByIdAsync(command.Id);
            await _Repository.DeleteAsync(client);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(client.Id);
        }
    }
}