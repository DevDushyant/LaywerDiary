using AspNetCoreHero.Results;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.CaseDetails
{
    public class DeleteCaseDocumentCommand : IRequest<Result<Guid>>
    {
        public Guid DocId { get; set; }
    }
    public class DeleteCaseDocumentCommandHandler : IRequestHandler<DeleteCaseDocumentCommand, Result<Guid>>
    {
        private readonly ICaseDocsRepository _CaseDocRepo;
        private readonly IUnitOfWork unitOfWork;
        public DeleteCaseDocumentCommandHandler(ICaseDocsRepository _CaseDocRepo, IUnitOfWork unitOfWork)
        {
            this._CaseDocRepo = _CaseDocRepo;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(DeleteCaseDocumentCommand request, CancellationToken cancellationToken)
        {
            var doc = _CaseDocRepo.Entities.Where(w => w.Id == request.DocId).FirstOrDefault();
            if (doc == null) return Result<Guid>.Fail("There is no document avaible for this Id");
            await _CaseDocRepo.DeleteAsync(doc);
            await unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success("Document deleted Successfully!");
        }
    }
}
