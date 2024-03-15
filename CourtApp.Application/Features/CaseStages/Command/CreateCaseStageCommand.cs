using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var mappeddata = mapper.Map<CaseStageEntity>(request);
            await repository.InsertAsync(mappeddata);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(mappeddata.Id);
        }
    }
}
