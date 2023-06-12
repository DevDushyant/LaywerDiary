﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Features.Queries.BookMasters
{
    public class GetAllBookMasterCacheResponse
    {
        public int Id { get; set; }
        public string BookType { get; set; }
        public string PublisherName { get; set; }
        public string BookName { get; set; }
        public int Year { get; set; }
    }
}
