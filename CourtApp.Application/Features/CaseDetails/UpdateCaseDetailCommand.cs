using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Features.Case;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace CourtApp.Application.Features.CaseDetails
{
    public class UpdateCaseDetailCommand:IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public DateTime InstitutionDate { get; set; }
        public Guid CourtTypeId { get; set; }
        public Guid CaseCategoryId { get; set; }
        public Guid CaseTypeId { get; set; }
        public Guid CourtBenchId { get; set; }
        public string CaseNo { get; set; }
        public int CaseYear { get; set; }
        public string FirstTitle { get; set; }
        public int FirstTitleCode { get; set; }
        public string SecondTitle { get; set; }
        public int SecoundTitleCode { get; set; }
        public string CisNumber { get; set; }
        public int CisYear { get; set; }
        public string CnrNumber { get; set; }
        public DateTime? NextDate { get; set; }
        public string CaseStageCode { get; set; }
        public Guid LinkedCaseId { get; set; }
        public Guid ClientId { get; set; }
        public ICollection<CaseAgainstEntityModel> AgainstCaseDetails { get; set; }
    }
    public class UpdateCaseDetailCommandHandler : IRequestHandler<UpdateCaseDetailCommand, Result<Guid>>
    {
        private readonly IUserCaseRepository _repository;
        private readonly IMapper _mapper;
        private IUnitOfWork _uow { get; set; }
        public UpdateCaseDetailCommandHandler(IUserCaseRepository _repository, IUnitOfWork _uow)
        {
            this._repository = _repository;
            this._uow= _uow;
        }

        public async Task<Result<Guid>> Handle(UpdateCaseDetailCommand request, CancellationToken cancellationToken)
        {
            var detail = await _repository.GetByIdAsync(request.Id);
            if (detail == null)
                return Result<Guid>.Fail($"Case detail Not Found.");
            else
            {
                detail.ClientId = request.ClientId;
                detail.InstitutionDate= request.InstitutionDate;
                detail.NextDate = request.NextDate;
                detail.CnrNumber = request.CnrNumber;
                await _repository.UpdateAsync(detail);
                await _uow.Commit(cancellationToken);
                return Result<Guid>.Success(detail.Id);
            }
        }
    }
}
