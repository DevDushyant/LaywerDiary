using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseStages.Command
{
    public class CreateCaseStageCommand : IRequest<Result<Guid>>
    {
        public string CaseStage { get; set; }
        public CreateCaseStageCommand()
        {

        }
    }

    public class CreateCaseStageCommandHandler : IRequestHandler<CreateCaseStageCommand, Result<Guid>>
    {
        private readonly ICaseStageRepository repository;
        private readonly IMapper mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public CreateCaseStageCommandHandler(ICaseStageRepository repository, IMapper mapper, IUnitOfWork _unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            this._unitOfWork = _unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateCaseStageCommand request, CancellationToken cancellationToken)
        {
            // Check if a record with the same name already exists (case-insensitive)
            var existingCadre = repository.QryEntities
                .Where(e => e.CaseStage.ToLower().Trim().Contains(request.CaseStage.ToLower().Trim()))
                .FirstOrDefault();

            if (existingCadre != null)
            {
                return Result<Guid>.Fail("Record already exists.");
            }

            // Map the request to the entity
            var newStage = mapper.Map<CaseStageEntity>(request);

            // Insert the new entity
            await repository.InsertAsync(newStage);

            // Commit the transaction
            await _unitOfWork.Commit(cancellationToken);

            return Result<Guid>.Success(newStage.Id);
        }
    }
}
