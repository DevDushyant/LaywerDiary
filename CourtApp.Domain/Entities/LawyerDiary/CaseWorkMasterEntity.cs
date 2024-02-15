﻿using AuditTrail.Abstrations;
using System.ComponentModel.DataAnnotations;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    public class CaseWorkMasterEntity : AuditableEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
