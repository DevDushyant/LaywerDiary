using AspNetCoreHero.Results;
using CourtApp.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.BookMasters.Commands
{
    public class UpdateBookMasterCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string PublisherName { get; set; }
        public int BookTypeId { get; set; }
        public int PublisherId { get; set; }
    }
    public class UpdateBookMasterCommandHandler : IRequestHandler<UpdateBookMasterCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookMasterRepository _Repository;

        public UpdateBookMasterCommandHandler(IBookMasterRepository _Repository, IUnitOfWork unitOfWork)
        {
            this._Repository = _Repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(UpdateBookMasterCommand command, CancellationToken cancellationToken)
        {
            var book = await _Repository.GetByIdAsync(command.Id);

            if (book == null)
            {
                return Result<int>.Fail($"Book Type Not Found.");
            }
            else
            {
                book.BookTypeId = command.BookTypeId;
                book.PublisherID = command.PublisherId;
                book.Year = command.Year;  
                await _Repository.UpdateAsync(book);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(book.Id);
            }
        }
    }
}
