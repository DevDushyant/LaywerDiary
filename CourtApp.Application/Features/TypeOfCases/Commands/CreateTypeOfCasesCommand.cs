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

namespace CourtApp.Application.Features.Typeofcasess.Commands
{
    public class CreateTypeOfCasesCommand : IRequest<Result<Guid>>
    {
        public Guid NatureId { get; set; }
        public Guid CourtTypeId { get; set; }
        public int StateId { get; set; }
        public List<TypeOfCase> CaseTypes { get; set; }

    }
    public class TypeOfCase
    {
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        public string Abbreviation { get; set; }
    }

    public class CreateCaseKindCommandHandler : IRequestHandler<CreateTypeOfCasesCommand, Result<Guid>>
    {
        private readonly ITypeOfCasesRepository repository;
        private readonly IMapper mapper;
        private readonly ICaseNatureRepository caseNatureRepository;
        private IUnitOfWork _unitOfWork { get; set; }

        public CreateCaseKindCommandHandler(ITypeOfCasesRepository repository,
            IMapper mapper, IUnitOfWork _unitOfWork, ICaseNatureRepository caseNatureRepository)
        {
            this.repository = repository;
            this.mapper = mapper;
            this._unitOfWork = _unitOfWork;
            this.caseNatureRepository = caseNatureRepository;
        }
        public async Task<Result<Guid>> Handle(CreateTypeOfCasesCommand request, CancellationToken cancellationToken)
        {
            if (request.CaseTypes.Count > 0)
            {
                Guid id = Guid.Empty;
                foreach (var c in request.CaseTypes)
                {
                    var detail = repository.QryEntities
                                .Where(w => w.Name_En.ToLower()
                                .Equals(c.Name_En.ToLower())
                                && w.CourtTypeId.Equals(request.CourtTypeId)
                                && w.NatureId.Equals(request.NatureId)
                                && w.Abbreviation.Equals(c.Abbreviation)
                                )
                                .FirstOrDefault() ?? null;
                    if (detail == null)
                    {
                        var cdt = new TypeOfCasesEntity()
                        {
                            Name_En = c.Name_En,
                            Name_Hn = c.Name_Hn,
                            CourtTypeId = request.CourtTypeId,
                            NatureId = request.NatureId,
                            Abbreviation = c.Abbreviation
                        };
                        await repository.InsertAsync(cdt);
                        await _unitOfWork.Commit(cancellationToken);
                        id = cdt.Id;
                    }
                    else
                        return Result<Guid>.Fail("Error! the Given name is already exist! "+ c.Name_En + " ");
                }
                return Result<Guid>.Success(id);
            }
            return Result<Guid>.Fail("Case type is not supplied!");
            //var isExist = repository.QryEntities
            //    .Where(w => (w.Abbreviation.Equals(request.Abbreviation.Trim()) ||
            //    w.Name_En.Equals(request.Name_En.Trim())) && w.CourtTypeId == request.CourtTypeId).FirstOrDefault();
            //if (isExist != null)
            //    return Result<Guid>.Fail("The given information is already exist");
            //var mappeddata = mapper.Map<TypeOfCasesEntity>(request);
            //await repository.InsertAsync(mappeddata);
            //await _unitOfWork.Commit(cancellationToken);
            //return Result<Guid>.Success(mappeddata.Id);
        }
    }
}
