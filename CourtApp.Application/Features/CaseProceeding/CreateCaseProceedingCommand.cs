using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseProceeding
{
    public class CreateCaseWorkCommand : IRequest<Result<Guid>>
    {
        public List<Guid> SelectedCases { get; set; }
        public List<Guid> SelectedSubHeads { get; set; }
        public Guid HeadId { get; set; }
        public Guid StageId { get; set; }
        public DateTime NextDate { get; set; }
        public string Remark { get; set; }
    }

    public class CreateCaseProceedingCommandHandler : IRequestHandler<CreateCaseWorkCommand, Result<Guid>>
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
        public async Task<Result<Guid>> Handle(CreateCaseWorkCommand request, CancellationToken cancellationToken)
        {
            List<CaseProcedingEntity> mappingEntity = new List<CaseProcedingEntity>();
            foreach (var CaseId in request.SelectedCases)
            {
                foreach (var subHeadId in request.SelectedSubHeads)
                {
                    var mpDt = new CaseProcedingEntity();
                    mpDt.Id = Guid.NewGuid();
                    mpDt.CaseId = CaseId;
                    mpDt.SubHeadId = subHeadId;
                    mpDt.HeadId = request.HeadId;
                    mpDt.StageId = request.StageId;
                    mpDt.NextDate = request.NextDate;
                    mpDt.Remark = request.Remark;
                    await _Repository.AddAsync(mpDt);                   
                }
            }            
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(mappingEntity.Select(s => s.CaseId).FirstOrDefault()); ;
        }
    }
}
