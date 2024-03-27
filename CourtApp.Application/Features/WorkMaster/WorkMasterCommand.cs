using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Enums;
using CourtApp.Application.Features.ProceedingHead;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMaster
{
    public class WorkMasterCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public int ActionType { get; set; }
        public string Work_En { get; set; }
        public string Work_Hn { get; set; }
    }
    public class CreateWorkMasterCommandHandler : IRequestHandler<WorkMasterCommand, Result<Guid>>
    {
        private readonly IWorkMasterRepository _Repository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public CreateWorkMasterCommandHandler(IWorkMasterRepository _Repository, IMapper _mapper, IUnitOfWork _unitOfWork)
        {
            this._mapper = _mapper;
            this._Repository = _Repository;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(WorkMasterCommand request, CancellationToken cancellationToken)
        {
            WorkMasterEntity entity = null;
            if (request.ActionType == ((int)ActionTypes.Add))
            {
                entity = _mapper.Map<WorkMasterEntity>(request);
                await _Repository.InsertAsync(entity);
            }
            if (request.ActionType == ((int)ActionTypes.Update))
            {
                entity = _Repository.GetByIdAsync(request.Id).Result;              
                entity.Work_En = request.Work_En;
                entity.Work_Hn = request.Work_Hn;
                await _Repository.UpdateAsync(entity);
            }
            //if (request.ActionType == ((int)ActionTypes.Update))
            //{
            //    entity = _Repository.GetByIdAsync(request.Id).Result;
            //    await _Repository.DeleteAsync(entity);
            //}
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(entity.Id);
        }
    }
}
