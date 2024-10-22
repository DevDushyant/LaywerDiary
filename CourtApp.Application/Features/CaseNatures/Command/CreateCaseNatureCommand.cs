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
    public class CreateCaseNatureCommand : IRequest<Result<Guid>>
    {
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public Guid CourtTypeId { get; set; }        
    }
    public class CreateCaseNatureCommandHandler : IRequestHandler<CreateCaseNatureCommand, Result<Guid>>
    {
        private readonly ICaseNatureRepository repository;
        private readonly IMapper mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public CreateCaseNatureCommandHandler(ICaseNatureRepository repository, IMapper mapper, IUnitOfWork _unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CreateCaseNatureCommand request, CancellationToken cancellationToken)
        {
            var IsExists = repository.CaseNatures
                .Where(c => c.Name_En.Contains(request.Name_En.Trim()) && c.CourtTypeId==request.CourtTypeId)
                .FirstOrDefault();
            if (IsExists == null)
            {
                var mappeddata = mapper.Map<NatureEntity>(request);
                await repository.InsertAsync(mappeddata);
                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(mappeddata.Id);
            }
            else
                return Result<Guid>.Fail($"{request.Name_En} is already exists.");
        }
    }

}
