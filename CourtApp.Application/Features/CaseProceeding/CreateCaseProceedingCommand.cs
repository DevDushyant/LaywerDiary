using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.DTOs.CaseProceedings;
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
        public Guid HeadId { get; set; }
        public Guid SubHeadId { get; set; }
        public Guid? StageId { get; set; }
        public DateTime? NextDate { get; set; }
        public string Remark { get; set; }
        public ProceedingWorkDto ProcWork { get; set; }
        public DateTime? ProceedingDate { get; set; }

    }

    public class CreateCaseProceedingCommandHandler : IRequestHandler<CreateCaseProceedingCommand, Result<Guid>>
    {
        private readonly ICaseProceedingRepository _Repository;
        private readonly IUserCaseRepository _CaseRepo;
        private readonly IProceedingHeadRepository _ProcRepo;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public CreateCaseProceedingCommandHandler(ICaseProceedingRepository _Repository,
            IMapper _mapper,
            IUnitOfWork _unitOfWork,
            IUserCaseRepository caseRepo,
            IProceedingHeadRepository procRepo)
        {
            this._Repository = _Repository;
            this._mapper = _mapper;
            this._unitOfWork = _unitOfWork;
            _CaseRepo = caseRepo;
            _ProcRepo = procRepo;
        }
        public async Task<Result<Guid>> Handle(CreateCaseProceedingCommand request, CancellationToken cancellationToken)
        {

            var ProcDetail = await _ProcRepo.GetByIdAsync(request.HeadId);
            if (ProcDetail != null && ProcDetail.Abbreviation == "DISP")
            {
                var CaseDetail = await _CaseRepo.GetByIdAsync(request.CaseId);
                CaseDetail.DisposalDate = DateTime.Now;
                await _CaseRepo.UpdateAsync(CaseDetail);
            }
            var entity = _mapper.Map<CaseProcedingEntity>(request);
            entity.ProceedingDate = request.ProceedingDate;
            await _Repository.AddAsync(entity);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(entity.Id); ;
        }
    }
}
