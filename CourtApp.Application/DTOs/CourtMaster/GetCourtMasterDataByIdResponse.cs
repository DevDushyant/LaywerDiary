using System;

namespace CourtApp.Application.DTOs.CourtMaster
{
    public class GetCourtMasterDataByIdResponse
    {
        public Guid CourtTypeId { get; set; }
        public int DistrictCode { get; set; }
        public int StateCode { get; set; }
        public string CourtName { get; set; }
        public string Bench { get; set; }
        public string HeadQuerter { get; set; }
        public string Address { get; set; }
    }
}
