using AspNetCoreHero.Results;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseWork
{
    public class UpdateCWorkStatusCommand : IRequest<Result<Guid>>
    {
        public List<Guid> CWorkId { get; set; }
        public int Status { get; set; }
        public Guid ProcId { get; set; }
    }

    public class UpdateCWorkStatusCommandHandler : IRequestHandler<UpdateCWorkStatusCommand, Result<Guid>>
    {
        private readonly ICaseWorkRepository _Repository;
        private readonly ICaseProceedingRepository _ProcRepo;
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateCWorkStatusCommandHandler(ICaseWorkRepository _Repository,
            IUnitOfWork _unitOfWork,
            ICaseProceedingRepository procRepo)
        {
            this._Repository = _Repository;
            this._unitOfWork = _unitOfWork;
            _ProcRepo = procRepo;
        }
        public async Task<Result<Guid>> Handle(UpdateCWorkStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = await _ProcRepo.GetDetailById(request.ProcId);
            if (entity != null)
            {
                List<ProcWorkEntity> w = new List<ProcWorkEntity>();
                foreach (var item in request.CWorkId)
                {
                    var pr = entity.ProcWork.Works
                        .Where(w => w.WorkId == item)
                        .FirstOrDefault();
                    if (pr != null)
                    {
                        pr.AppliedOn = DateTime.Now;
                        pr.Status = request.Status;
                        w.Add(pr);
                    }
                }
                entity.ProcWork.Works = w;
                await _ProcRepo.UpdateAsync(entity);                

                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(entity.Id);
            }           
            return Result<Guid>.Fail("There is no proceeding Id");
        }
    }
}
