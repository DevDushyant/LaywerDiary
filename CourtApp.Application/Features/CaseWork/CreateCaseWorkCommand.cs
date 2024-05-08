using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseWork
{
    public class CreateCaseWorkCommand : IRequest<Result<Guid>>
    {
        public Guid CaseId { get; set; }
        public Guid WorkId { get; set; }
        public List<Guid> SubWorkId { get; set; }
        public DateTime WorkingDate { get; set; }
        public string Remark { get; set; }
    }

    public class CreateCaseWorkCommandHandler : IRequestHandler<CreateCaseWorkCommand, Result<Guid>>
    {
        private readonly ICaseWorkRepository _Repository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public CreateCaseWorkCommandHandler(ICaseWorkRepository _Repository, IMapper _mapper, IUnitOfWork _unitOfWork)
        {
            this._Repository = _Repository;
            this._mapper = _mapper;
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CreateCaseWorkCommand request, CancellationToken cancellationToken)
        {
            foreach (var subId in request.SubWorkId)
            {
                var mpDt = new CaseWorkEntity();
                mpDt.Id = Guid.NewGuid();
                mpDt.CaseId = request.CaseId;
                mpDt.WorkId = request.WorkId;
                mpDt.SubWorkId = subId;
                mpDt.WorkingDate = request.WorkingDate;
                mpDt.Remark = request.Remark;
                await _Repository.InsertAsync(mpDt);
            }
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(); ;
        }
    }
}
