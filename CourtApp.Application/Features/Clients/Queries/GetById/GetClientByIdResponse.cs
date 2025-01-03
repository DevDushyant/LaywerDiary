using CourtApp.Application.DTOs.Client;
using System;

namespace CourtApp.Application.Features.Clients.Queries.GetById
{
    public class GetClientByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string OfficeEmail { get; set; }
        public string Phone { get; set; }
        public string ReferalBy { get; set; }
        public Guid AppearenceID { get; set; }
        public Guid OppositCounselId { get; set; }
        public ClientFeeDto Fees { get; set; }
    }    
}