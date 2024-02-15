using AuditTrail.Abstrations;
using CourtApp.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{

    [Table("m_client", Schema = "ld")]
    public class ClientEntity : AuditableEntity
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string OfficeEmail { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }

        [ForeignKey("State")]
        public string StateCode { get; set; }
        public virtual StateEntity State { get; set; }

        [ForeignKey("District")]
        public int DistrictCode { get; set; }
        public virtual DistrictEntity District { get; set; }
        public string Address { get; set; }
       
    }
}