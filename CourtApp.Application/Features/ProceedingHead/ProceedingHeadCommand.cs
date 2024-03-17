using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Enums;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.ProceedingHead
{
    public class ProceedingHeadCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public int ActionType { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }

    public class CreateProceedingHeadCommandHandler : IRequestHandler<ProceedingHeadCommand, Result<Guid>>
    {
        private readonly IProceedingHeadRepository _Repository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public CreateProceedingHeadCommandHandler(IProceedingHeadRepository _Repository, IMapper _mapper, IUnitOfWork _unitOfWork)
        {
            this._mapper = _mapper;
            this._Repository = _Repository;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(ProceedingHeadCommand request, CancellationToken cancellationToken)
        {
            ProceedingHeadEntity entity = null;
            if (request.ActionType == ((int)ActionTypes.Add))
            {
                entity = _mapper.Map<ProceedingHeadEntity>(request);
                await _Repository.InsertAsync(entity);
            }
            if (request.ActionType == ((int)ActionTypes.Update))
            {
                entity = _Repository.GetByIdAsync(request.Id).Result;
                entity.Name_En = request.Name_En;
                entity.Name_Hn = request.Name_Hn;
                await _Repository.UpdateAsync(entity);
            }
            if (request.ActionType == ((int)ActionTypes.Update))
            {
                entity = _Repository.GetByIdAsync(request.Id).Result;
                await _Repository.DeleteAsync(entity);
            }
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(entity.Id);
        }
    }
}
