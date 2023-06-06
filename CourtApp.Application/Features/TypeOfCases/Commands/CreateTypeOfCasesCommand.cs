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
    public class CreateTypeOfCasesCommand:IRequest<Result<int>>
    {
        public int CaseNatureId { get; set; }        
        public string Typeofcases { get; set; }

        public CreateTypeOfCasesCommand()
        {

        }
    }

    public class CreateCaseKindCommandHandler : IRequestHandler<CreateTypeOfCasesCommand, Result<int>>
    {
        private readonly ITypeOfCasesRepository repository;
        private readonly IMapper mapper;
        private IUnitOfWork _unitOfWork { get; set; }

        public CreateCaseKindCommandHandler(ITypeOfCasesRepository repository, IMapper mapper, IUnitOfWork _unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<int>> Handle(CreateTypeOfCasesCommand request, CancellationToken cancellationToken)
        {
            var mappeddata = mapper.Map<TypeOfCasesEntity>(request);
            await repository.InsertAsync(mappeddata);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(mappeddata.Id);
        }
    }
}
