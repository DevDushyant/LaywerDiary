using System;

namespace CourtApp.Application.Features.Clients.Queries.GetById
{
    public class GetClientByIdResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Dob { get; set; }
        public string FatherName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string OfficeEmail { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string LandMark { get; set; }
        public string HouseNo { get; set; }
        public string Address { get; set; }      
        public int StateCode { get; set; }
        public int DistrictCode { get; set; }

        //public string CountryCode { get; set; }
        //public SelectList ReferedBy { get; set; }
        //public SelectList Appearence { get; set; }
        //public SelectList Narration { get; set; }
    }
}