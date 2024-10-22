using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.CaseDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseProceeding
{
    public class CreateCaseProceedingCommand : IRequest<Result<Guid>>
    {
        public Guid CaseId { get; set; }
        public Guid ProceedingTypeId { get; set; }
        public List<Guid> ProceedingsIds { get; set; }
        public Guid StageId { get; set; }
        public DateTime NextDate { get; set; }
        public string Remark { get; set; }
    }

    public class CreateCaseProceedingCommandHandler : IRequestHandler<CreateCaseProceedingCommand, Result<Guid>>
    {
        private readonly ICaseProceedingRepository _Repository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public CreateCaseProceedingCommandHandler(ICaseProceedingRepository _Repository, IMapper _mapper, IUnitOfWork _unitOfWork)
        {
            this._Repository = _Repository;
            this._mapper = _mapper;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CreateCaseProceedingCommand request, CancellationToken cancellationToken)
        {
            List<CaseProcedingEntity> mappingEntity = new List<CaseProcedingEntity>();
            foreach (var subHeadId in request.ProceedingsIds)
            {
                var mpDt = new CaseProcedingEntity() { CreatedBy = "" };
                mpDt.Id = Guid.NewGuid();
                mpDt.CaseId = request.CaseId;
                mpDt.SubHeadId = subHeadId;
                mpDt.HeadId = request.ProceedingTypeId;
                mpDt.StageId = request.StageId;
                mpDt.NextDate = request.NextDate;
                mpDt.Remark = request.Remark;
                await _Repository.AddAsync(mpDt);
            }
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(mappingEntity.Select(s => s.CaseId).FirstOrDefault()); ;
        }
    }
}
