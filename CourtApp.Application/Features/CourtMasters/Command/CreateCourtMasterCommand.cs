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

namespace CourtApp.Application.Features.CourtMasters.Command
{
    public class CreateCourtMasterCommand : IRequest<Result<int>>
    {
        public int CourtTypeId { get; set; }
        public string CourtName { get; set; }
        public string CourtFullName { get; set; }
        public string Bench { get; set; }
        public string HeadQuerter { get; set; }
        public string Address { get; set; }
        public int DistrictCode { get; set; }
        public string StateCode { get; set; }
        public CreateCourtMasterCommand()
        {

        }
    }

    public class CreateCourtMasterCommandHandler : IRequestHandler<CreateCourtMasterCommand, Result<int>>
    {
        private readonly ICourtMasterRepository repository;
        private readonly IMapper mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public CreateCourtMasterCommandHandler(ICourtMasterRepository repository, IMapper mapper, IUnitOfWork _unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<int>> Handle(CreateCourtMasterCommand request, CancellationToken cancellationToken)
        {
            var mappeddata = mapper.Map<CourtMasterEntity>(request);           
            await repository.InsertAsync(mappeddata);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(mappeddata.Id);
        }
    }
}
