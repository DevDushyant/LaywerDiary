using AspNetCoreHero.Results;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseWork
{
    public class UpdateCWorkStatusCommand : IRequest<Result<Guid>>
    {
        public List<Guid> CWorkId { get; set; }
        public int Status { get; set; }
    }

    public class UpdateCWorkStatusCommandHandler : IRequestHandler<UpdateCWorkStatusCommand, Result<Guid>>
    {
        private readonly ICaseWorkRepository _Repository;
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateCWorkStatusCommandHandler(ICaseWorkRepository _Repository, IUnitOfWork _unitOfWork)
        {
            this._Repository = _Repository;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(UpdateCWorkStatusCommand request, CancellationToken cancellationToken)
        {
            var cdEntityt = new CaseWorkEntity();
            foreach (var item in request.CWorkId)
            {
                cdEntityt = await _Repository.GetByIdAsync(item);
                if (cdEntityt == null)
                    return Result<Guid>.Fail($"Case Work Id is not found.");
                else
                {
                    cdEntityt.Status = request.Status;
                    cdEntityt.AppliedOn = DateTime.Now;
                    await _Repository.UpdateAsync(cdEntityt);
                }
            }
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(cdEntityt.Id);
        }
    }
}
