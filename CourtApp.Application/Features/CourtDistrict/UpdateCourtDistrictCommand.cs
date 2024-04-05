using AspNetCoreHero.Results;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtDistrict
{
    public class UpdateCourtDistrictCommand:IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public string Abbreviation { get; set; }
    }
    public class UpdateCourtDistrictCommandHandler : IRequestHandler<UpdateCourtDistrictCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourtDistrictRepository repository;

        public UpdateCourtDistrictCommandHandler(ICourtDistrictRepository repository,
            IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            _unitOfWork = unitOfWork;           
        }

        public async Task<Result<Guid>> Handle(UpdateCourtDistrictCommand command, CancellationToken cancellationToken)
        {
            var detailById = await repository.GetByIdAsync(command.Id);
            if (detailById == null)
                return Result<Guid>.Fail($"Court District not found.");
            else
            {                
                detailById.DistrictId=command.DistrictId;
                detailById.StateId=command.StateId;
                detailById.Abbreviation=command.Abbreviation;
                detailById.Name_En=command.Name_En;
                detailById.Name_Hn=command.Name_Hn;                
                await repository.UpdateAsync(detailById);
                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(detailById.Id);
            }
        }
    }
}
