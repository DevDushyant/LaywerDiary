using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtFeeStructure.Command
{
    public class CreateCourtFeeStructureCommand : IRequest<Result<Guid>>
    {
        public string StateCode { get; set; }
        public Double MinValue { get; set; }
        public Double MaxValue { get; set; }
        public Double Rate { get; set; }
        public Double FixAmount { get; set; }
    }

    public class CreateCourtFeeStructureCommandHandler : IRequestHandler<CreateCourtFeeStructureCommand, Result<Guid>>
    {
        private readonly ICourtFeeStructureRepository repository;
        private readonly IMapper mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public CreateCourtFeeStructureCommandHandler(ICourtFeeStructureRepository repository, IMapper mapper, IUnitOfWork _unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            this._unitOfWork = _unitOfWork;

        }
        public async Task<Result<Guid>> Handle(CreateCourtFeeStructureCommand request, CancellationToken cancellationToken)
        {
            var mappeddata = mapper.Map<CourtFeeStructureEntity>(request);
            await repository.InsertAsync(mappeddata);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(mappeddata.Id);
        }
    }
}
