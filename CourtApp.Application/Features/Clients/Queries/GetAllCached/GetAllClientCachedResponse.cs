namespace CourtApp.Application.Features.Clients.Queries.GetAllCached
{
    public class GetAllClientCachedResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string OfficeEmail { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string LandMark { get; set; }
        public string HouseNo { get; set; }
        public string StateCode { get; set; }
        public int DistrictCode { get; set; }
        public int CityId { get; set; }
    }
}