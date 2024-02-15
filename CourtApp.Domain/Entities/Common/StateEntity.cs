using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Entities.Common
{
    [Table("m_state")]
    public class StateEntity :AuditableEntity
    {        
        public string StateCode { get; set; }
        public String StateName { get; set; } 
    }
}