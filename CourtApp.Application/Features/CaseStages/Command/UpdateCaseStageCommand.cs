using AspNetCoreHero.Results;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseStages.Command
{
    public class UpdateCaseStageCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string CaseStage { get; set; }
        public UpdateCaseStageCommand()
        {

        }
    }

    public class UpdateCaseStageCommandHandler : IRequestHandler<UpdateCaseStageCommand, Result<int>>
    {
        private readonly ICaseStageRepository repository;
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateCaseStageCommandHandler(ICaseStageRepository repository, IUnitOfWork _unitOfWork)
        {
            this.repository = repository;
            this._unitOfWork = _unitOfWork;
        }

        public async Task<Result<int>> Handle(UpdateCaseStageCommand request, CancellationToken cancellationToken)
        {
            var naturedetail = await repository.GetByIdAsync(request.Id);
            if (naturedetail == null)
                return Result<int>.Fail($"Case stage detail Not Found.");
            else
            {
                naturedetail.CaseStage = request.CaseStage;
                await repository.UpdateAsync(naturedetail);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(naturedetail.Id);
            }
        }
    }
}
