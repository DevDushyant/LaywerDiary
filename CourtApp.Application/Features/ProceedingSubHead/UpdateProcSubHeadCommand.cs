using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.ProceedingSubHead
{
    public class UpdateProcSubHeadCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public Guid HeadId { get; set; }
        public int ActionType { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
    public class UpdateProcSubHeadCommandHandler : IRequestHandler<UpdateProcSubHeadCommand, Result<Guid>>
    {
        private readonly IProceedingSubHeadRepository _Repository;       
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateProcSubHeadCommandHandler(IProceedingSubHeadRepository _Repository, IMapper _mapper, IUnitOfWork _unitOfWork)
        {
            
            this._Repository = _Repository;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(UpdateProcSubHeadCommand request, CancellationToken cancellationToken)
        {
            var detailById = await _Repository.GetByIdAsync(request.Id);
            if (detailById == null)
                return Result<Guid>.Fail($"Proceeding sub head not found.");
            else
            {
                detailById.Name_En = request.Name_En;
                detailById.Name_Hn = request.Name_Hn;
                detailById.HeadId = request.HeadId;
                
                await _Repository.UpdateAsync(detailById);
                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(detailById.Id);
            }
        }
    }
}
