using CourtApp.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Domain.Entities.LawyerDiary;
using System;

namespace CourtApp.Application.Features.Clients.Commands
{
    public partial class CreateClientCommand : IRequest<Result<Guid>>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Dob { get; set; }
        public string Email { get; set; }
        public string OfficeEmail { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public int StateCode { get; set; }
        public int DistrictCode { get; set; }
        public Guid CaseId { get; set; }
    }

    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Result<Guid>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IStateMasterRepository _rStateMst;
        private readonly IDistrictMasterRepository _rDistrictMst;
        private readonly IUserCaseRepository _repository;

        private IUnitOfWork _unitOfWork { get; set; }
        public CreateClientCommandHandler(IClientRepository _clientRepository, IUnitOfWork unitOfWork
            , IMapper mapper, IStateMasterRepository rStateMst, IDistrictMasterRepository _rDistrictMst
            , IUserCaseRepository repository)
        {
            this._clientRepository = _clientRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _rStateMst = rStateMst;
            this._rDistrictMst = _rDistrictMst;
            _repository = repository;
        }

        public async Task<Result<Guid>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ClientEntity>(request);
            entity.State = _rStateMst.GetStateById(request.StateCode);
            entity.District = _rDistrictMst.GetDistrictById(request.DistrictCode);
            await _clientRepository.InsertAsync(entity);
            await _unitOfWork.Commit(cancellationToken);
            if (entity.Id != Guid.Empty)
            {
                var detail = await _repository.GetByIdAsync(request.CaseId);
                if (detail == null)
                    return Result<Guid>.Fail($"Case detail Not Found.");
                else
                {
                    detail.ClientId = entity.Id;
                    await _repository.UpdateAsync(detail);
                    await _unitOfWork.Commit(cancellationToken);                    
                }
            }
            return Result<Guid>.Success(entity.Id);
        }
    }
}