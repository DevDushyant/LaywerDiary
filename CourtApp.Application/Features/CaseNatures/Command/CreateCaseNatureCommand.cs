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
    public class CreateCaseNatureCommand : IRequest<Result<int>>
    {
        public string CaseNature { get; set; }
    }
    public class CreateCaseNatureCommandHandler : IRequestHandler<CreateCaseNatureCommand, Result<int>>
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
        public async Task<Result<int>> Handle(CreateCaseNatureCommand request, CancellationToken cancellationToken)
        {
            var mappeddata = mapper.Map<CaseNatureEntity>(request);
            await repository.InsertAsync(mappeddata);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(mappeddata.Id);
        }
    }

}
