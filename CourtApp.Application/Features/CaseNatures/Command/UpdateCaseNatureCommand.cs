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
   public class UpdateCaseNatureCommand:IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }

    public class UpdateCaseNatureCommandCommandHandler : IRequestHandler<UpdateCaseNatureCommand, Result<Guid>>
    {
        private readonly ICaseNatureRepository repository;       
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateCaseNatureCommandCommandHandler(ICaseNatureRepository repository,IUnitOfWork _unitOfWork)
        {
            this.repository = repository;           
            this._unitOfWork = _unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateCaseNatureCommand request, CancellationToken cancellationToken)
        {
            var Detail = await repository.GetByIdAsync(request.Id);
            if (Detail == null)
                return Result<Guid>.Fail($"Case nature detail Not Found.");
            else
            {
                Detail.Name_En = request.Name_En;
                Detail.Name_Hn = request.Name_Hn;
                await repository.UpdateAsync(Detail);
                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(Detail.Id);
            }
        }
    }
}
