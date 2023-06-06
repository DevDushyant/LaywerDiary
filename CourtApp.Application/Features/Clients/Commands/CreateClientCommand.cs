using CourtApp.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Domain.Entities.LawyerDiary;

namespace CourtApp.Application.Features.Clients.Commands
{
    public partial class CreateClientCommand : IRequest<Result<int>>
    {
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

    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Result<int>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateClientCommandHandler(IClientRepository _clientRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._clientRepository = _clientRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var client = _mapper.Map<ClientEntity>(request);
            await _clientRepository.InsertAsync(client);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(client.Id);
        }
    }
}