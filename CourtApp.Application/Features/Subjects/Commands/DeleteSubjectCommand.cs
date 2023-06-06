using CourtApp.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Subjects.Commands
{
    public class DeleteSubjectCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }    
    }

    public class DeleteSubjectCommandHandler : IRequestHandler<DeleteSubjectCommand, Result<int>>
    {
        private readonly ISubjectRepository _Repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSubjectCommandHandler(ISubjectRepository _Repository, IUnitOfWork unitOfWork)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteSubjectCommand command, CancellationToken cancellationToken)
        {
            var booktype = await _Repository.GetByIdAsync(command.Id);
            await _Repository.DeleteAsync(booktype);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(booktype.Id);
        }
    }
}
