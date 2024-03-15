using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtFeeStructure.Command
{
    public class UpdateCourtFeeStructureCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public int StateCode { get; set; }
        public Double MinValue { get; set; }
        public Double MaxValue { get; set; }
        public Double Rate { get; set; }
        public Double FixAmount { get; set; }
    }

    public class UpdateCourtFeeStructureCommandHandler : IRequestHandler<UpdateCourtFeeStructureCommand, Result<Guid>>
    {
        private readonly ICourtFeeStructureRepository repository;
       private IUnitOfWork _unitOfWork { get; set; }
        public UpdateCourtFeeStructureCommandHandler(ICourtFeeStructureRepository repository,IUnitOfWork _unitOfWork)
        {
            this.repository = repository;          
            this._unitOfWork = _unitOfWork;

        }
        public async Task<Result<Guid>> Handle(UpdateCourtFeeStructureCommand request, CancellationToken cancellationToken)
        {
            var detail = await repository.GetByIdAsync(request.Id);
            if (detail == null)
                return Result<Guid>.Fail($"Fee structure detail Not Found.");
            else
            {               
                detail.State.Code = request.StateCode;
                detail.MaxValue = request.MaxValue;
                detail.MinValue = request.MinValue;
                detail.Rate = request.Rate;
                detail.FixAmount = request.FixAmount;
                detail.LastModifiedOn = DateTime.Now;
                await repository.UpdateAsync(detail);
                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(detail.Id);
            }
        }
    }
}
