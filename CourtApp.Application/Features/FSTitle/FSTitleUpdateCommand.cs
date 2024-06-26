using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.FSTitle
{
    public class FSTitleUpdateCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public int TypeId { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
    public class FSTitleUpdateCommandHandler : IRequestHandler<FSTitleUpdateCommand, Result<Guid>>
    {
        private readonly IFSTitleRepository _Repo;
        private readonly IMapper _mapper;
        private IUnitOfWork _uow { get; set; }
        public FSTitleUpdateCommandHandler(IFSTitleRepository _Repo, IMapper _mapper, IUnitOfWork _uow)
        {
            this._mapper = _mapper;
            this._Repo = _Repo;
            this._uow = _uow;
        }
        public async Task<Result<Guid>> Handle(FSTitleUpdateCommand command, CancellationToken cancellationToken)
        {
            var entity = await _Repo.GetByIdAsync(command.Id);

            if (entity == null)
                return Result<Guid>.Fail($"First & Secound Title Not Found.");
            else
            {
                entity.Name_En = command.Name_En ?? entity.Name_En;
                entity.Name_Hn = command.Name_Hn ?? entity.Name_Hn;
                entity.TypeId = (command.TypeId == 0) ? entity.TypeId : command.TypeId;
                await _Repo.UpdateAsync(entity);
                await _uow.Commit(cancellationToken);
                return Result<Guid>.Success(entity.Id);
            }
        }
    }
}
