using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Cadre
{
    public class CadreCreateCommand : IRequest<Result<Guid>>
    {
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
    public class CadreCreateCommandHandler : IRequestHandler<CadreCreateCommand, Result<Guid>>
    {
        private readonly ICadreMasterRepository repository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public CadreCreateCommandHandler(ICadreMasterRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CadreCreateCommand request, CancellationToken cancellationToken)
        {
            var CadreIsExist = repository
                .Entities
                .Where(w => w.Name_En.Equals(request.Name_En.Trim()))
                .FirstOrDefault();
            if (CadreIsExist != null) return Result<Guid>.Fail("Record is already exist");
            else
            {
                var mEntity = mapper.Map<CadreMasterEntity>(request);
                await repository.InsertAsync(mEntity);
                await unitOfWork.Commit(cancellationToken);
                return Result<Guid>.Success(mEntity.Id);
            }
        }
    }
}
