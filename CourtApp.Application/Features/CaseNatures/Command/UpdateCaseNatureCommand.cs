using AspNetCoreHero.Results;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseNatures.Command
{
    public class UpdateCaseNatureCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public Guid CourtTypeId { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }

    public class UpdateCaseNatureCommandCommandHandler : IRequestHandler<UpdateCaseNatureCommand, Result<Guid>>
    {
        private readonly ICaseNatureRepository repository;
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateCaseNatureCommandCommandHandler(ICaseNatureRepository repository, IUnitOfWork _unitOfWork)
        {
            this.repository = repository;
            this._unitOfWork = _unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateCaseNatureCommand request, CancellationToken cancellationToken)
        {
            // Fetch the record to update
            var existingRecord = await repository.GetByIdAsync(request.Id);
            if (existingRecord == null)
                return Result<Guid>.Fail("Record does not exist.");


            // Check for name conflict with other records (excluding the current record)
            var duplicateRecord = await repository.CaseNatures
                .Where(e => e.Id != request.Id && e.CourtTypeId.Equals(request.CourtTypeId) && e.Name_En.ToLower() == request.Name_En.ToLower().Trim())
                .FirstOrDefaultAsync(cancellationToken);

            if (duplicateRecord != null)
                return Result<Guid>.Fail("Another record with the same name already exists.");

            // Update the entity fields
            existingRecord.Name_En = request.Name_En;
            existingRecord.CourtTypeId = request.CourtTypeId;

            await repository.UpdateAsync(existingRecord);
            await _unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(existingRecord.Id);
        }
    }
}
