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

namespace CourtApp.Application.Features.Typeofcasess.Commands
{
    public class UpdateTypeOfCasesCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public Guid NatureId { get; set; }        
        public string Name_En { get; set; }        
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
    }

    public class UpdateTypeofcasesCommandHandler : IRequestHandler<UpdateTypeOfCasesCommand, Result<Guid>>
    {
        private readonly ITypeOfCasesRepository repository;
        private readonly ICaseNatureRepository caseNatureRepository;
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateTypeofcasesCommandHandler(ITypeOfCasesRepository repository,
            IUnitOfWork _unitOfWork,
            ICaseNatureRepository caseNatureRepository)
        {
            this.repository = repository;
            this._unitOfWork = _unitOfWork;
            this.caseNatureRepository = caseNatureRepository;
        }

        public async Task<Result<Guid>> Handle(UpdateTypeOfCasesCommand request, CancellationToken cancellationToken)
        {
            var Detail = await repository.GetByIdAsync(request.Id);
            if (Detail == null)
                return Result<Guid>.Fail($"Case kind detail Not Found.");
            else
            {
                var nature = caseNatureRepository.GetByIdAsync(request.NatureId).Result;
                if (nature != null)
                Detail.Nature =nature;
                Detail.Name_En=request.Name_En;
                Detail.Name_Hn=request.Name_Hn;
                Detail.Abbreviation=request.Abbreviation;
                await repository.UpdateAsync(Detail);
                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(Detail.Id);
            }
        }
    }
}
