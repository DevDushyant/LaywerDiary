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
    public class CreateTypeOfCasesCommand:IRequest<Result<Guid>>
    {
        public Guid NatureId { get; set; }
        public Guid CourtTypeId { get; set; }
        public int StateId { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
    }

    public class CreateCaseKindCommandHandler : IRequestHandler<CreateTypeOfCasesCommand, Result<Guid>>
    {
        private readonly ITypeOfCasesRepository repository;
        private readonly IMapper mapper;
        private readonly ICaseNatureRepository caseNatureRepository;
        private IUnitOfWork _unitOfWork { get; set; }

        public CreateCaseKindCommandHandler(ITypeOfCasesRepository repository, 
            IMapper mapper, IUnitOfWork _unitOfWork, ICaseNatureRepository caseNatureRepository)
        {
            this.repository = repository;
            this.mapper = mapper;
            this._unitOfWork = _unitOfWork;
            this.caseNatureRepository = caseNatureRepository;
        }
        public async Task<Result<Guid>> Handle(CreateTypeOfCasesCommand request, CancellationToken cancellationToken)
        {
            var mappeddata = mapper.Map<TypeOfCasesEntity>(request);
            var nature = caseNatureRepository.GetByIdAsync(request.NatureId).Result;
            mappeddata.Nature = nature;
            await repository.InsertAsync(mappeddata);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(mappeddata.Id);
        }
    }
}
