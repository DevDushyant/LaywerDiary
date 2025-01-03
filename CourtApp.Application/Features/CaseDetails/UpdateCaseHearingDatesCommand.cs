using AspNetCoreHero.Results;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails
{
    public class UpdateCaseHearingDatesCommand : IRequest<Result<Guid>>
    {
        public List<CaseHearingDto> CasesHearingDt { get; set; }
    }
    public class CaseHearingDto
    {
        public Guid CaseId { get; set; }
        public DateTime HearingDt { get; set; }
    }
    public class UpdateCaseHearingDatesCommandHandler : IRequestHandler<UpdateCaseHearingDatesCommand, Result<Guid>>
    {
        private readonly IUserCaseRepository _Repository;
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateCaseHearingDatesCommandHandler(IUserCaseRepository _Repository, IUnitOfWork _unitOfWork)
        {
            this._Repository = _Repository;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(UpdateCaseHearingDatesCommand request, CancellationToken cancellationToken)
        {
            Guid Id = Guid.Empty;
            foreach (var item in request.CasesHearingDt)
            {
                var entity = await _Repository.GetByIdAsync(item.CaseId);
                if (entity == null)
                    return Result<Guid>.Fail($"Case is not found.");
                else
                {
                    entity.NextDate = item.HearingDt;
                    await _Repository.UpdateAsync(entity);
                    Id = entity.Id;
                }
            }
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(Id);
        }
    }
}
