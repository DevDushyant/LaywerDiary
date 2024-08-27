using AspNetCoreHero.Results;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseWork
{
    public class UpdateCopyingStatusCommand : IRequest<Result<Guid>>
    {
        public List<Guid> CaseId { get; set; }
        public int Status { get; set; }
    }
    public class UpdateCopyingStatusCommandHandler : IRequestHandler<UpdateCopyingStatusCommand, Result<Guid>>
    {
        private readonly ICaseWorkRepository _Repository;
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateCopyingStatusCommandHandler(ICaseWorkRepository _Repository, IUnitOfWork _unitOfWork)
        {
            this._Repository = _Repository;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(UpdateCopyingStatusCommand request, CancellationToken cancellationToken)
        {
            var cdEntityt = new CaseWorkEntity();
            foreach (var item in request.CaseId)
            {
                cdEntityt = _Repository.Entities.Where(c => c.CaseId == item).FirstOrDefault();
                if (cdEntityt == null)
                    return Result<Guid>.Fail($"Case Work Id is not found.");
                else
                {
                    cdEntityt.Status = request.Status;
                    cdEntityt.ReceivedOn = DateTime.Now;
                    await _Repository.UpdateAsync(cdEntityt);
                }
            }
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(cdEntityt.Id);
        }
    }
}
