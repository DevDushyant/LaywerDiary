using CourtApp.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace CourtApp.Application.Features.Clients.Commands
{
    public class UpdateCreateClientCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string OfficeEmail { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string StateCode { get; set; }
        public int DistrictCode { get; set; }        
    }
    public class UpdateClientCommandHandler : IRequestHandler<UpdateCreateClientCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClientRepository _clientRepository;

        public UpdateClientCommandHandler(IClientRepository _clientRepository, IUnitOfWork unitOfWork)
        {
            this._clientRepository = _clientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateCreateClientCommand command, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(command.Id);

            if (client == null)
            {
                return Result<Guid>.Fail($"Client Not Found.");
            }
            else
            {
                client.FirstName = command.FirstName ?? client.FirstName;
                client.LastName = command.LastName ?? client.LastName;
                client.Email = command.Email ?? client.Email;
                client.OfficeEmail = command.OfficeEmail ?? client.OfficeEmail;
                client.Phone = command.Phone ?? client.Phone;
                client.Mobile = command.Mobile ?? client.Mobile;
                client.State.Code = client.State.Code;
                client.District.Code = command.DistrictCode;

                await _clientRepository.UpdateAsync(client);
                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(client.Id);
            }
        }
    }
}