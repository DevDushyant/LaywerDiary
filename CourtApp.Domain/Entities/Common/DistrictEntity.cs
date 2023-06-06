using AspNetCoreHero.Abstractions.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Entities.Common
{
    [Table("Mst_District",Schema = "Common")]
    public class DistrictEntity 
    {        
        public int DistrictCode { get; set; }
        public string DistrictName { get; set; }        
                
        [ForeignKey("State")]
        public string StateCode { get; set; }
        public virtual StateEntity State { get; set; }
    }
}