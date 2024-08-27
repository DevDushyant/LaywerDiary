using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CourtType.Command
{
    public class CreateCourtTypeCommand : IRequest<Result<Guid>>
    {
        public string CourtType { get; set; }
    }
    public class CreateCourtTypeCommandHandler : IRequestHandler<CreateCourtTypeCommand, Result<Guid>>
    {
        private readonly ICourtTypeRepository _Repository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }
        public CreateCourtTypeCommandHandler(ICourtTypeRepository _Repository, IMapper _mapper, IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
            this._mapper = _mapper;
            this._Repository = _Repository;
        }
        public async Task<Result<Guid>> Handle(CreateCourtTypeCommand request, CancellationToken cancellationToken)
        {
            var IsExists = _Repository.CourtTypeEntities
                .Where(c => c.CourtType.Contains(request.CourtType.Trim()))
                .FirstOrDefault();
            if (IsExists == null)
            {
                var CourtType = _mapper.Map<CourtTypeEntity>(request);
                await _Repository.InsertAsync(CourtType);
                await _unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(CourtType.Id);
            }
            else
                return Result<Guid>.Fail($"{request.CourtType} is already exists.");
        }
    }
}
