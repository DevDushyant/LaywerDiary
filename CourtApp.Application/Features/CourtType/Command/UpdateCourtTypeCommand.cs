using AspNetCoreHero.Results;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtType.Command
{
    public class UpdateCourtTypeCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public string CourtType { get; set; }
    }

    public class UpdateCourtTypeCommandHandler : IRequestHandler<UpdateCourtTypeCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourtTypeRepository _Repository;

        public UpdateCourtTypeCommandHandler(ICourtTypeRepository _Repository, IUnitOfWork unitOfWork)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(UpdateCourtTypeCommand command, CancellationToken cancellationToken)
        {
            // Fetch the record to update
            var existingRecord = await _Repository.GetByIdAsync(command.Id);
            if (existingRecord == null)
                return Result<Guid>.Fail("Record does not exist.");


            // Check for name conflict with other records (excluding the current record)
            var duplicateRecord = await _Repository.CourtTypeEntities
                .Where(e => e.Id != command.Id && e.CourtType.ToLower() == command.CourtType.ToLower().Trim())
                .FirstOrDefaultAsync(cancellationToken);

            if (duplicateRecord != null)
                return Result<Guid>.Fail("Another record with the same name already exists.");

            // Update the entity fields
            existingRecord.CourtType = command.CourtType;

            await _Repository.UpdateAsync(existingRecord);
            await _unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(existingRecord.Id);
        }
    }
}
