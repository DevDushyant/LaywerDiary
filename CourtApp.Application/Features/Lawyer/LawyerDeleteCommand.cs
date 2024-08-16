using AspNetCoreHero.Results;
using CourtApp.Application.Features.FSTitle;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Lawyer
{
    public class LawyerDeleteCommand:IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }
    public class LawyerDeleteCommandHandler : IRequestHandler<LawyerDeleteCommand, Result<Guid>>
    {
        private readonly ILawyerRepository _Repo;
        private IUnitOfWork _uow { get; set; }
        public LawyerDeleteCommandHandler(ILawyerRepository _Repo, IUnitOfWork _uow)
        {
            this._Repo = _Repo;
            this._uow = _uow;
        }
        public async Task<Result<Guid>> Handle(LawyerDeleteCommand request, CancellationToken cancellationToken)
        {
            var enity = await _Repo.GetByIdAsync(request.Id);
            await _Repo.DeleteAsync(enity);
            await _uow.Commit(cancellationToken);
            return Result<Guid>.Success(enity.Id);
        }
    }
}
