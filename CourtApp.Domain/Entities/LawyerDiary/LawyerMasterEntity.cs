using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_lawyer", Schema = "ld")]
    public class LawyerMasterEntity : AuditableEntity
    {        
        public new Guid Id { get; set; }        
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EnrollNumber { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
