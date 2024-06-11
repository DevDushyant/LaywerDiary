using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.DOType
{
    public class UpdateDOTypeCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public int TypeId { get; set; }
        public string Name_En { get; set; }
    }
    public class UpdateDOTypeCommandHandler : IRequestHandler<UpdateDOTypeCommand, Result<Guid>>
    {
        private readonly IDOTypeRepository _Repo;
        private readonly IMapper _mapper;
        private IUnitOfWork _uow { get; set; }
        public UpdateDOTypeCommandHandler(IDOTypeRepository _Repo, IMapper _mapper, IUnitOfWork _uow)
        {
            this._mapper = _mapper;
            this._Repo = _Repo;
            this._uow = _uow;
        }
        public async Task<Result<Guid>> Handle(UpdateDOTypeCommand command, CancellationToken cancellationToken)
        {
            var entity = await _Repo.GetByIdAsync(command.Id);

            if (entity == null)
                return Result<Guid>.Fail($"Draft & Order Not Found.");
            else
            {
                entity.Name_En = command.Name_En ?? entity.Name_En;
                entity.TypeId = (command.TypeId == 0) ? entity.TypeId : command.TypeId;
                await _Repo.UpdateAsync(entity);
                await _uow.Commit(cancellationToken);
                return Result<Guid>.Success(entity.Id);
            }
        }
    }
}
