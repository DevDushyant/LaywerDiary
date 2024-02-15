using AuditTrail.Abstrations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Entities.Common
{
    [Table("m_district")]
    public class DistrictEntity:AuditableEntity 
    {        
        public int DistrictCode { get; set; }
        public string DistrictName { get; set; }        
                
        [ForeignKey("State")]
        public string StateCode { get; set; }
        public virtual StateEntity State { get; set; }
    }
}