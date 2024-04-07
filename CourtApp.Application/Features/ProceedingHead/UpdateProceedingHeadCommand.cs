using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.ProceedingHead
{
    public class UpdateProceedingHeadCommand:IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
    public class UpdateProceedingHeadCommandHandler : IRequestHandler<UpdateProceedingHeadCommand, Result<Guid>>
    {
        private readonly IProceedingHeadRepository repository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public UpdateProceedingHeadCommandHandler(IProceedingHeadRepository repository, IMapper _mapper, IUnitOfWork _unitOfWork)
        {
            this._mapper = _mapper;
            this.repository = repository;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(UpdateProceedingHeadCommand request, CancellationToken cancellationToken)
        {
            var detailById = await repository.GetByIdAsync(request.Id);
            if (detailById == null)
                return Result<Guid>.Fail($"Proceeding head not found.");
            else
            {               
                detailById.Name_En = request.Name_En;
                detailById.Name_Hn = request.Name_Hn;
                await repository.UpdateAsync(detailById);
                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(detailById.Id);
            }
        }
    }
}
