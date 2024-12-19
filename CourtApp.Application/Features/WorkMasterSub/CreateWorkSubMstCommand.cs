using AspNetCoreHero.Results;
using AutoMapper;
using CourtApp.Application.Interfaces.Repositories;
using CourtApp.Domain.Entities.LawyerDiary;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.WorkMasterSub
{
    public class CreateWorkSubMstCommand : IRequest<Result<Guid>>
    {
        public Guid WorkId { get; set; }
        //public required string Name_En { get; set; }
        //public string Name_Hn { get; set; }
        //public string Abbreviation { get; set; }
        public List<WrkSubMaster> Works { get; set; }
    }
    public class WrkSubMaster
    {
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        //public string Abbreviation { get; set; }
    }
    public class CreateWorkSubMstCommandHandler : IRequestHandler<CreateWorkSubMstCommand, Result<Guid>>
    {
        private readonly IWorkMasterSubRepository repository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public CreateWorkSubMstCommandHandler(IWorkMasterSubRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CreateWorkSubMstCommand request, CancellationToken cancellationToken)
        {
            if (request.Works.Count > 0)
            {
                Guid id = Guid.Empty;
                foreach (var c in request.Works)
                {
                    var detail = repository.Entities
                                .Where(w => w.Name_En.ToLower()
                                .Equals(c.Name_En.ToLower())
                                && w.WorkId.Equals(request.WorkId))
                                .FirstOrDefault() ?? null;
                    if (detail == null)
                    {
                        var cdt = new WorkMasterSubEntity()
                        {
                            Name_En = c.Name_En,
                            Name_Hn = c.Name_Hn,
                            WorkId = request.WorkId,
                        };
                        await repository.InsertAsync(cdt);
                        await unitOfWork.Commit(cancellationToken);
                        id = cdt.Id;
                    }
                    else
                        return Result<Guid>.Fail("Error! the Given name is already exist! " + c.Name_En+" ");
                }
                return Result<Guid>.Success(id);
            }
            return Result<Guid>.Fail("Work type is not supplied!");
            //var wsmdt =repository.Entities
            //           .Where(x => (x.WorkId == request.WorkId && x.Name_En.Equals(request.Name_En)) 
            //            || (x.Abbreviation.Equals(request.Abbreviation)))
            //            .FirstOrDefault();
            //if (wsmdt != null) return Result<Guid>.Fail("The provided detail already exist!");
            //var mappeddata = mapper.Map<WorkMasterSubEntity>(request);
            //await repository.InsertAsync(mappeddata);
            //await unitOfWork.Commit(cancellationToken);
            //return Result<Guid>.Success(mappeddata.Id);
        }
    }
}
