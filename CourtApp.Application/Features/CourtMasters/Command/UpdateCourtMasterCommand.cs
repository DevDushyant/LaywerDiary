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
    public class UpdateCourtMasterCommand : IRequest<Result<Guid>>
    {
        public Guid UId { get; set; }
        public Guid CourtTypeId { get; set; }
        public string CourtName { get; set; }
        public string CourtFullName { get; set; }
        public string Bench { get; set; }
        public string HeadQuerter { get; set; }
        public string Address { get; set; }
        public int DistrictCode { get; set; }
        public int StateCode { get; set; }
    }

    public class UpdateCourtMasterCommandHandler : IRequestHandler<UpdateCourtMasterCommand, Result<Guid>>
    {
        private readonly ICourtMasterRepository repository;
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateCourtMasterCommandHandler(ICourtMasterRepository repository, IUnitOfWork _unitOfWork)
        {
            this.repository = repository;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(UpdateCourtMasterCommand request, CancellationToken cancellationToken)
        {
            var detail = await repository.GetByIdAsync(request.UId);
            if (detail == null)
                return Result<Guid>.Fail($"Court detail Not Found.");
            else
            {
                detail.CourtType.Id = request.CourtTypeId;
                detail.Name_En = request.CourtName;
                detail.Address = request.Address;
                detail.HeadQuerter = request.HeadQuerter;
                detail.Bench = request.Bench;
                detail.District.Code = request.DistrictCode;
                detail.State.Code = request.StateCode;
                detail.LastModifiedOn = DateTime.Now;
                await repository.UpdateAsync(detail);
                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(detail.Id);
            }
        }
    }
}
