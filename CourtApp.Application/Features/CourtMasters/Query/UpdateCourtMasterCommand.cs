using AspNetCoreHero.Results;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtMasters.Command
{
    public class UpdateCourtMasterCommand : IRequest<Result<int>>
    {
        public Guid UniqueId { get; set; }
        public int CourtTypeId { get; set; }
        public string CourtName { get; set; }
        public string CourtFullName { get; set; }
        public string Bench { get; set; }
        public string HeadQuerter { get; set; }
        public string Address { get; set; }
        public int DistrictCode { get; set; }
        public string StateCode { get; set; }
    }

    public class UpdateCourtMasterCommandHandler : IRequestHandler<UpdateCourtMasterCommand, Result<int>>
    {
        private readonly ICourtMasterRepository repository;
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateCourtMasterCommandHandler(ICourtMasterRepository repository, IUnitOfWork _unitOfWork)
        {
            this.repository = repository;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<int>> Handle(UpdateCourtMasterCommand request, CancellationToken cancellationToken)
        {
            var detail = await repository.GetByIdAsync(request.UniqueId);
            if (detail == null)
                return Result<int>.Fail($"Court detail Not Found.");
            else
            {
                detail.CourtTypeId = request.CourtTypeId;
                detail.CourtName = request.CourtName;
                detail.Address = request.Address;
                detail.HeadQuerter = request.HeadQuerter;
                detail.Bench = request.Bench;
                detail.DistrictCode = request.DistrictCode;
                detail.StateCode = request.StateCode;
                detail.LastModifiedOn = DateTime.Now;
                await repository.UpdateAsync(detail);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(detail.Id);
            }
        }
    }
}
