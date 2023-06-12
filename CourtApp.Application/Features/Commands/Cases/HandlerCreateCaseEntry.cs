using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Commands.Cases
{
    internal class HandlerCreateCaseEntry : IRequestHandler<CommandCreateCaseEntry, Result<Guid>>
    {
        private readonly IUserCaseRepository _Repository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public HandlerCreateCaseEntry(IUserCaseRepository _Repository, IMapper _mapper, IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
            this._mapper = _mapper;
            this._Repository = _Repository;
        }
        public async Task<Result<Guid>> Handle(CommandCreateCaseEntry request, CancellationToken cancellationToken)
        {
            var userCaseEntry = _mapper.Map<CaseEntity>(request);
            await _Repository.InsertAsync(userCaseEntry);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(userCaseEntry.Id);
        }
    }
}
