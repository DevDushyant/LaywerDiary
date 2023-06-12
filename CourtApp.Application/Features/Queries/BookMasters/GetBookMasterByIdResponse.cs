using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Queries.BookMasters
{
    public class GetBookMasterByIdResponse
    {
        public int Id { get; set; }
        public int BookTypeId { get; set; }
        public int PublisherId { get; set; }
        public string BookName { get; set; }
        public int Year { get; set; }
    }
}
