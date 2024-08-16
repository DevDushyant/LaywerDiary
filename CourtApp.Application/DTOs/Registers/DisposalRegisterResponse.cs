using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.DTOs.Registers
{
    public class DisposalRegisterResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Reason { get; set; }
        public string DisposalDate { get; set; }
    }
}
