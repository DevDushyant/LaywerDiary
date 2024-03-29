using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Enums;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseManagment
{
    public class CaseManagmentCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public int ActionType { get; set; }
        public DateTime InstitutionDate { get; set; }
        public Guid ClientId { get; set; }
        public Guid NatureId { get; set; }
        public Guid TypeCaseId { get; set; }
        public Guid CourtTypeId { get; set; }
        public Guid CourtId { get; set; }
        public Guid CaseTypeId { get; set; }
        public string Number { get; set; }
        public int CaseYear { get; set; }
        public string FirstTitle { get; set; }
        public int TitleTypeFirst { get; set; }
        public string SecondTitle { get; set; }
        public int TitleTypeSecond { get; set; }
        public DateTime NextDate { get; set; }
        public string CaseStageCode { get; set; }
        public List<AgainstCaseDecision> AgainstCaseDetails { get; set; }
    }

    public class AgainstCaseDecision
    {
        public new Guid Id { get; set; }
        public Guid clientcaseid { get; set; }
        public DateTime? CaseAgainstDecisionDate { get; set; }
        public string AgainstCaseNumber { get; set; }
        public int? AgainstYear { get; set; }
        // public Guid LinkedCaseId { get; set; }
        public Guid AgainstCourtTypeId { get; set; }
        public Guid AgainstCourtId { get; set; }
        //public Guid LinkedCaseId { get; set; }
    }
    public class CreateCaseManagmentCommandHandler : IRequestHandler<CaseManagmentCommand, Result<Guid>>
    {
        private readonly ICaseManagmentRepository _Repository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public CreateCaseManagmentCommandHandler(ICaseManagmentRepository _Repository, IMapper _mapper, IUnitOfWork _unitOfWork)
        {
            this._mapper = _mapper;
            this._Repository = _Repository;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CaseManagmentCommand request, CancellationToken cancellationToken)
        {
            CaseEntity entity = null;
            if (request.ActionType == ((int)ActionTypes.Add))
            {
                entity = _mapper.Map<CaseEntity>(request);
                await _Repository.InsertAsync(entity);
            }
            if (request.ActionType == ((int)ActionTypes.Update))
            {
                entity = _Repository.GetByIdAsync(request.Id).Result;              
                //entity.Work_En = request.Work_En;
                //entity.Work_Hn = request.Work_Hn;
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
