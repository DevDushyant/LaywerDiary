using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.Common;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtComplex
{
    public class CreateCourtComplexCommand : IRequest<Result<Guid>>
    {
        public int StateId { get; set; }
        //public int DistrictId { get; set; }
        public Guid CourtDistrictId { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
    }
    public class CreateCourtComplexCommandHandler : IRequestHandler<CreateCourtComplexCommand, Result<Guid>>
    {
        private readonly ICourtComplexRepository repository;
        private readonly IMapper mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public CreateCourtComplexCommandHandler(ICourtComplexRepository repository, IMapper mapper, IUnitOfWork _unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CreateCourtComplexCommand request, CancellationToken cancellationToken)
        {
            var detail = repository.Entities
                .Where(w => w.Abbreviation.ToLower()
                        .Equals(request.Abbreviation.ToLower()))
                .FirstOrDefault() ?? null;
            if (detail == null)
            {
                var entity = mapper.Map<CourtComplexEntity>(request);
                await repository.InsertAsync(entity);
                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(entity.Id);
            }
            return Result<Guid>.Fail("Entered abbreviation already exist!");
        }
    }
}
