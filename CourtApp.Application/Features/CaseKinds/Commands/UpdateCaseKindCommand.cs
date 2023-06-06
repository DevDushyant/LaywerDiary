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

namespace CourtApp.Application.Features.CaseKinds.Commands
{
    public class UpdateCaseKindCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string CaseKind { get; set; }
        public int CourtTypeId { get; set; }
        public UpdateCaseKindCommand()
        {

        }

    }

    public class UpdateCaseKindCommandHandler : IRequestHandler<UpdateCaseKindCommand, Result<int>>
    {
        private readonly ICaseKindRepository repository;
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateCaseKindCommandHandler(ICaseKindRepository repository, IUnitOfWork _unitOfWork)
        {
            this.repository = repository;
            this._unitOfWork = _unitOfWork;
        }

        public async Task<Result<int>> Handle(UpdateCaseKindCommand request, CancellationToken cancellationToken)
        {
            var naturedetail = await repository.GetByIdAsync(request.Id);
            if (naturedetail == null)
                return Result<int>.Fail($"Case kind detail Not Found.");
            else
            {
                naturedetail.CaseKind = request.CaseKind;
                await repository.UpdateAsync(naturedetail);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(naturedetail.Id);
            }
        }
    }
}
