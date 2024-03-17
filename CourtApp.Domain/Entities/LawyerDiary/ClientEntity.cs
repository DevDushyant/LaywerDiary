using AuditTrail.Abstrations;
using CourtApp.Entities.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{

    [Table("client", Schema = "ld")]    
    public class ClientEntity : AuditableEntity
    {
        public new Guid Id { get; set; }
        public required string FirstName { get; set; }
        public string MiddleName { get; set; }
        public required string LastName { get; set; }
        public required string FatherName { get; set; }
        public required string Dob { get; set; }
        public required string Email { get; set; }
        public required string Mobile { get; set; }
        public string OfficeEmail { get; set; }
        public string Phone { get; set; }
        public bool IsRural { get; set; }
        public string Landmark { get; set; }

        #region Foreign keys Area
        [ForeignKey("StateCode")]
        public virtual StateEntity State { get; set; }

        [ForeignKey("DistrictCode")]
        public virtual DistrictEntity District { get; set; }       
        #endregion
    }
}