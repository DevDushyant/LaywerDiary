using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.FSTitle
{
    public class FSTitleCreateCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }        
        public int TypeId { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }

    public class FSTitleCreateCommandHandler : IRequestHandler<FSTitleCreateCommand, Result<Guid>>
    {
        private readonly IFSTitleRepository _Repo;
        private readonly IMapper _mapper;
        private IUnitOfWork _uow { get; set; }
        public FSTitleCreateCommandHandler(IFSTitleRepository _Repo, 
            IMapper _mapper,
            IUnitOfWork _uow)
        {
            this._Repo = _Repo;
            this._mapper = _mapper;
            this._uow = _uow;
        }
        public async Task<Result<Guid>> Handle(FSTitleCreateCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<FSTitleEntity>(request);
            await _Repo.InsertAsync(entity);
            await _uow.Commit(cancellationToken);
            return Result<Guid>.Success(entity.Id);
        }
    }
}
