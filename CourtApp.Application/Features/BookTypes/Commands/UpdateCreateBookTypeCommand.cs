using CourtApp.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.BookTypes.Commands
{
    public class UpdateCreateBookTypeCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string BookType { get; set; }       

        public class UpdateBookTypeCommandHandler : IRequestHandler<UpdateCreateBookTypeCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IBookTypeRepository _bookTypeRepository;

            public UpdateBookTypeCommandHandler(IBookTypeRepository _bookTypeRepository, IUnitOfWork unitOfWork)
            {
                this._bookTypeRepository = _bookTypeRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateCreateBookTypeCommand command, CancellationToken cancellationToken)
            {
                var bookType = await _bookTypeRepository.GetByIdAsync(command.Id);

                if (bookType == null)
                {
                    return Result<int>.Fail($"Book Type Not Found.");
                }
                else
                {
                    bookType.BookType = command.BookType ?? bookType.BookType;                   
                    await _bookTypeRepository.UpdateAsync(bookType);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(bookType.Id);
                }
            }
        }
    }
}