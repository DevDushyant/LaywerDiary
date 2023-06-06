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

namespace CourtApp.Application.Features.CaseNatures.Command
{
   public class UpdateCaseNatureCommand:IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string CaseNature { get; set; }
    }

    public class UpdateCaseNatureCommandCommandHandler : IRequestHandler<UpdateCaseNatureCommand, Result<int>>
    {
        private readonly ICaseNatureRepository repository;       
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateCaseNatureCommandCommandHandler(ICaseNatureRepository repository,IUnitOfWork _unitOfWork)
        {
            this.repository = repository;           
            this._unitOfWork = _unitOfWork;
        }

        public async Task<Result<int>> Handle(UpdateCaseNatureCommand request, CancellationToken cancellationToken)
        {
            var naturedetail = await repository.GetByIdAsync(request.Id);
            if (naturedetail == null)
                return Result<int>.Fail($"Case nature detail Not Found.");
            else
            {
                naturedetail.CaseNature = request.CaseNature;
                await repository.UpdateAsync(naturedetail);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(naturedetail.Id);
            }
        }
    }
}
