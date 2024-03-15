using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    public class CaseWorkMasterEntity : AuditableEntity
    {
        public new Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
