using CourtApp.Web.Areas.LawyerDiary.Models;
using System.Collections.Generic;

namespace CourtApp.Web.Models
{
    public class DataTableRequest
    {
        public int Draw { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string SearchValue { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
    }
    public class Search
    {
        public string Value { get; set; }
    }

    public class Order
    {
        public int Column { get; set; }
        public string Dir { get; set; }
    }

    public class Column
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Orderable { get; set; }
    }

    public class DataTableResponse
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<ProceedingSubHeadViewModel> data { get; set; }
    }
}
