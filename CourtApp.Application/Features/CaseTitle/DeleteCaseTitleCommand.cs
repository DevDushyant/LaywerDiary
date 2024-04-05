using AspNetCoreHero.Results;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseTitle
{
    
    public class CaseTitleCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }
    public class CaseTitleCommandHandler : IRequestHandler<CaseTitleCommand, Result<Guid>>
    {
        private readonly ICaseTitleRepository _Repository;
        private readonly IUnitOfWork _unitOfWork;

        public CaseTitleCommandHandler(ICaseTitleRepository _Repository,
            IUnitOfWork unitOfWork)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CaseTitleCommand cmd, CancellationToken cancellationToken)
        {
            var detail = await _Repository.GetByIdAsync(cmd.Id);
            await _Repository.DeleteAsync(detail);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(detail.Id);
        }
    }
}
