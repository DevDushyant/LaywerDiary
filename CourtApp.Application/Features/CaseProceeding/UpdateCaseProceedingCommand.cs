using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.DTOs.CaseProceedings;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace CourtApp.Application.Features.CaseProceeding
{
    public class UpdateCaseProceedingCommand : IRequest<Result<Guid>>
    {
        public Guid CaseId { get; set; }
        public Guid HeadId { get; set; }
        public Guid SubHeadId { get; set; }
        public Guid? StageId { get; set; }
        public DateTime? NextDate { get; set; }
        public string Remark { get; set; }
        public ProceedingWorkDto ProcWork { get; set; }
    }
    public class UpdateCaseProceedingCommandHandler : IRequestHandler<UpdateCaseProceedingCommand, Result<Guid>>
    {
        private readonly ICaseProceedingRepository _Repository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateCaseProceedingCommandHandler(ICaseProceedingRepository _Repository, IMapper _mapper, IUnitOfWork _unitOfWork)
        {
            this._Repository = _Repository;
            this._mapper = _mapper;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(UpdateCaseProceedingCommand request, CancellationToken cancellationToken)
        {
            var entity = await _Repository.GetByIdAsync(request.CaseId,null);
            if (entity != null && entity.NextDate==request.NextDate)
            {
                entity.NextDate = request.NextDate;
                entity.HeadId = request.HeadId;
                entity.SubHeadId = request.SubHeadId;
                entity.StageId = request.StageId;
                entity.ProceedingDate = request.NextDate;
                entity.ProcWork = _mapper.Map<ProceedingWorkEntity>(request.ProcWork);
                await _Repository.UpdateAsync(entity);
                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(entity.Id);
            }
            else
            {
                var obj = _mapper.Map<CaseProcedingEntity>(request);
                obj.ProceedingDate = entity.NextDate;
                await _Repository.AddAsync(obj);
                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(obj.Id); ;
            }
        }
    }
}
