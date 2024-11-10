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
        private readonly ICaseProceedingRepository _ProcRepo;
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateCopyingStatusCommandHandler(ICaseWorkRepository _Repository,
            IUnitOfWork _unitOfWork, ICaseProceedingRepository _ProcRepo)
        {
            this._Repository = _Repository;
            this._unitOfWork = _unitOfWork;
            this._ProcRepo = _ProcRepo;
        }
        public async Task<Result<Guid>> Handle(UpdateCopyingStatusCommand request, CancellationToken cancellationToken)
        {
            //var cdEntityt = new CaseWorkEntity() { CreatedBy = "" };
            //foreach (var item in request.CaseId)
            //{
            //    cdEntityt = _Repository.Entities.Where(c => c.CaseId == item).FirstOrDefault();
            //    if (cdEntityt == null)
            //        return Result<Guid>.Fail($"Case Work Id is not found.");
            //    else
            //    {
            //        cdEntityt.Status = request.Status;
            //        cdEntityt.ReceivedOn = DateTime.Now;
            //        await _Repository.UpdateAsync(cdEntityt);
            //    }
            //}
            //await _unitOfWork.Commit(cancellationToken);
            //return Result<Guid>.Success(cdEntityt.Id);
            foreach (var it in request.CaseId)
            {
                var entity = await _ProcRepo.GetByIdAsync(it,null);
                if (entity != null)
                {
                    List<ProcWorkEntity> w = new List<ProcWorkEntity>();
                    var work = entity.ProcWork.Works;
                    foreach (var workEntity in work)
                    {
                        workEntity.ReceivedOn = DateTime.Now;
                        workEntity.Status = request.Status;
                        w.Add(workEntity);
                    }
                    entity.ProcWork.Works = w;
                    await _ProcRepo.UpdateAsync(entity);
                }
            }
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success();
        }
    }
}
