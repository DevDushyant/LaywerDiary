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
        public string UserId { get; set; }

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
            var LcasesDt = GetAllChildrenAsync(request.CaseId, request.UserId);
            if (LcasesDt != null)
            {
                foreach (var it in LcasesDt)
                {
                    entity.CaseId = it.Id;
                    await _Repository.AddAsync(entity);
                    await _unitOfWork.Commit(cancellationToken);
                }
            }

            return Result<Guid>.Success(entity.Id); ;
        }

        public List<CaseDetailEntity> GetAllChildrenAsync(Guid parentId, string UserId)
        {
            var UserCases = _CaseRepo.Entites.Where(w => w.CreatedBy.Equals(UserId)).ToList();
            return GetChildrenRecursive(UserCases, parentId);
        }

        private List<CaseDetailEntity> GetChildrenRecursive(List<CaseDetailEntity> UserCases, Guid parentId)
        {
            var children = UserCases.Where(c => c.LinkedCaseId == parentId).ToList();
            foreach (var child in children)
            {
                children.AddRange(GetChildrenRecursive(UserCases, child.Id));
            }
            return children;
        }
        //public async Task<(CaseDetailEntity Parent, List<CaseDetailEntity> Siblings)> GetParentAndSiblingsAsync(int childId)
        //{
        //    var child = await _context.Categories
        //        .Include(c => c.Parent)
        //        .FirstOrDefaultAsync(c => c.Id == childId);

        //    if (child?.ParentId == null)
        //    {
        //        return (null, new List<CaseDetailEntity>()); // No parent, so no siblings
        //    }

        //    var siblings = await _context.Categories
        //        .Where(c => c.ParentId == child.ParentId && c.Id != childId)
        //        .ToListAsync();

        //    return (child.Parent, siblings);
        //}


    }
}
