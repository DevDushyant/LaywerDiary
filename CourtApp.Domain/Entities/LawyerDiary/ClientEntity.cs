using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.Account;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{

    [Table("client", Schema = "ld")]    
    public class ClientEntity : AuditableEntity
    {
        
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string OfficeEmail { get; set; }
        public string Phone { get; set; }
        public string ReferalBy { get; set; }
        public Guid AppearenceID { get; set; }
        public Guid OppositCounselId { get; set; }       
        public virtual LawyerMasterEntity OppositCounsel { get; set; }        
        public virtual CaseFeeEntity CaseFee { get; set; }
    }
}