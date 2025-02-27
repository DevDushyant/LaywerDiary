using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Clients.Commands
{
    public class UpdateClientCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
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
        public string ReferalBy { get; set; }
        public Guid AppearenceID { get; set; }
        //public ClientFee FeeDetail { get; set; }
    }
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IStateMasterRepository _rStateMst;
        private readonly IDistrictMasterRepository _rDistrictMst;

        public UpdateClientCommandHandler(IClientRepository _clientRepository,
            IUnitOfWork unitOfWork, IStateMasterRepository rStateMst, IDistrictMasterRepository _rDistrictMst,
            IMapper _mapper)
        {
            this._clientRepository = _clientRepository;
            _unitOfWork = unitOfWork;
            this._rDistrictMst = _rDistrictMst;
            this._rStateMst = rStateMst;
            this._mapper = _mapper;

        }

        public async Task<Result<Guid>> Handle(UpdateClientCommand command, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(command.Id);
            if (client == null)
                return Result<Guid>.Fail($"Client Not Found.");
            else
            {

                client.Email = command.Email;
                client.OfficeEmail = command.OfficeEmail;
                client.Phone = command.Phone;
                client.Mobile = command.Mobile;
                client.ReferalBy = command.ReferalBy;
                //client.AppearenceID = command.AppearenceID;
                client.Address = command.Address;
                //client.CaseFee = _mapper.Map<CaseFeeEntity>(command.FeeDetail);
                await _clientRepository.UpdateAsync(client);
                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(client.Id);
            }
        }
    }
}