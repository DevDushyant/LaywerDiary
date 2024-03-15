using AuditTrail.Abstrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_c_type", Schema = "ld")]
    
    public class TypeOfCasesEntity : AuditableEntity
    {       
        public new Guid Id { get; set; }
        public required string Name_En { get; set; }
        public string Name_Hn { get; set; }

        [ForeignKey("NatureId")]
        public virtual NatureEntity Nature { get; set; }
    }
}