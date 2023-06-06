using AspNetCoreHero.Results;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Publications.Command
{
    public class DeletePublicationCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    public class DeletePublicationCommandHandler : IRequestHandler<DeletePublicationCommand, Result<int>>
    {
        private readonly IPublicationRepository _Repository;
        private readonly IUnitOfWork _unitOfWork;
        public DeletePublicationCommandHandler(IPublicationRepository _Repository, IUnitOfWork _unitOfWork)
        {
            this._Repository = _Repository;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<int>> Handle(DeletePublicationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _Repository.GetByIdAsync(request.Id);
            await _Repository.DeleteAsync(entity);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(entity.Id);
        }
    }
}
