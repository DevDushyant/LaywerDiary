using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Commands.Cases
{
    internal class HandlerDeleteCaseEntry : IRequestHandler<CommandDeleteCaseEntry, Result<Guid>>
    {
        private readonly IUserCaseRepository _Repository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public HandlerDeleteCaseEntry(IUserCaseRepository _Repository, IMapper _mapper, IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
            this._mapper = _mapper;
            this._Repository = _Repository;
        }
        public async Task<Result<Guid>> Handle(CommandDeleteCaseEntry command, CancellationToken cancellationToken)
        {
            var CaseEntryDetail = await _Repository.GetByIdAsync(command.Id);

            if (CaseEntryDetail == null)
            {
                return Result<Guid>.Fail($"Case Entry Not Found.");
            }
            else
            {  
                await _Repository.DeleteAsync(CaseEntryDetail);
                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(CaseEntryDetail.Id);
            }
        }

    }
}
