using CourtApp.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CourtApp.Domain.Entities.LawyerDiary;
using System;
using CourtApp.Domain.Entities.Account;
using System.Linq;
using CourtApp.Domain.Entities.Common;

namespace CourtApp.Application.Features.Clients.Commands
{
    public class CreateClientByCaseIdCommand : IRequest<Result<Guid>>
    {
        public Guid ClientId { get; set; }
        public string UserId { get; set; }
    }


    public class CreateClientByCaseIdCommandHandler : IRequestHandler<CreateClientByCaseIdCommand, Result<Guid>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IUserCaseRepository _repository;

        private IUnitOfWork _unitOfWork { get; set; }
        public CreateClientByCaseIdCommandHandler(IClientRepository _clientRepository, IUnitOfWork unitOfWork
            , IMapper mapper, IStateMasterRepository rStateMst, IDistrictMasterRepository _rDistrictMst
            , IUserCaseRepository repository)
        {
            this._clientRepository = _clientRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Result<Guid>> Handle(CreateClientByCaseIdCommand request, CancellationToken cancellationToken)
        {
            var detail = await _clientRepository.GetByIdAsync(request.ClientId);
            if (detail != null)
            {
                //var entity = _mapper.Map<ClientEntity>(request);                
                detail.Id = Guid.NewGuid();
                detail.ReferalBy = "";
                await _clientRepository.InsertAsync(detail);
                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(detail.Id);
            }
            return Result<Guid>.Fail("Error! The client is already exist for the logged in user");
        }
    }
}