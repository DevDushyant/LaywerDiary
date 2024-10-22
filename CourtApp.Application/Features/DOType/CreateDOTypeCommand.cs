using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.DTOs.DOTypes;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.DOType
{
    public class CreateDOTypeCommand : IRequest<Result<Guid>>
    {
        public int TypeId { get; set; }
        public string Name_En { get; set; }
    }

    public class CreateDOTypeCommandHandler : IRequestHandler<CreateDOTypeCommand, Result<Guid>>
    {
        private readonly IDOTypeRepository _Repo;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }

        public CreateDOTypeCommandHandler(IDOTypeRepository _Repo, IMapper _mapper, IUnitOfWork _unitOfWork)
        {
            this._Repo = _Repo; 
            this._mapper = _mapper; 
            this._unitOfWork = _unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CreateDOTypeCommand request, CancellationToken cancellationToken)
        {
            var info = _Repo.Entities.Where(w=>w.TypeId==request.TypeId && w.Name_En.Equals(request.Name_En)).FirstOrDefault();
            if (info != null) return Result<Guid>.Fail("Supplied info already exists");
            var entity = _mapper.Map<DOTypeEntity>(request);
            await _Repo.InsertAsync(entity);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(entity.Id);
        }
    }
}
