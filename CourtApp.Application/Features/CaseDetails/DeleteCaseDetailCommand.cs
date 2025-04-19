using AspNetCoreHero.Results;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails
{
    public class DeleteCaseDetailCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteCaseDetailCommandHandler : IRequestHandler<DeleteCaseDetailCommand, Result<Guid>>
    {
        private readonly IUserCaseRepository _Repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICaseDocsRepository _caseDocRepo;
        private readonly ICaseProceedingRepository caseProceeding;

        public DeleteCaseDetailCommandHandler(IUserCaseRepository _Repository, IUnitOfWork unitOfWork,
            ICaseDocsRepository caseDocRepo, ICaseProceedingRepository caseProceeding)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
            _caseDocRepo = caseDocRepo;
            this.caseProceeding = caseProceeding;
        }

        public async Task<Result<Guid>> Handle(DeleteCaseDetailCommand command, CancellationToken cancellationToken)
        {
            var detail = await _Repository.GetByIdAsync(command.Id);
            if (detail == null) return Result<Guid>.Fail("Record is not found for deletion!");
            var caseDocs = _caseDocRepo.Entities.Where(w => w.CaseId.Equals(command.Id)).ToList();
            if (caseDocs.Count > 0)
                await _caseDocRepo.DeleteRangeAsync(caseDocs);
            var procData = await caseProceeding.GetProceedingByCaseIdAsync(command.Id);
            if (procData.Any())
                await caseProceeding.DeleteRangeAsync(procData);

            await _Repository.DeleteAsync(detail);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(detail.Id);
        }
    }
}
