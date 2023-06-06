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
    public class CreateCaseKindCommand:IRequest<Result<int>>
    {
        public int CourtTypeId { get; set; }
        public string CaseKind { get; set; }

        public CreateCaseKindCommand()
        {

        }
    }

    public class CreateCaseKindCommandHandler : IRequestHandler<CreateCaseKindCommand, Result<int>>
    {
        private readonly ICaseKindRepository repository;
        private readonly IMapper mapper;
        private IUnitOfWork _unitOfWork { get; set; }

        public CreateCaseKindCommandHandler(ICaseKindRepository repository, IMapper mapper, IUnitOfWork _unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<int>> Handle(CreateCaseKindCommand request, CancellationToken cancellationToken)
        {
            var mappeddata = mapper.Map<CaseKindEntity>(request);
            await repository.InsertAsync(mappeddata);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(mappeddata.Id);
        }
    }
}
