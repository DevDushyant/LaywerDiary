using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using AuditTrail.Abstrations;
using System.Collections.Generic;

namespace CourtApp.Domain.Entities.Common
{
    [Table("m_village")]
    [Index(nameof(Code), IsUnique = true)]
    public class VillageEntity : AuditableEntity
    {
        public int Code { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        [ForeignKey("GpCode")]
        public virtual GPEntity Gp { get; set; }
        public ICollection<HabitationEntity> Havitations { get; set; }
    }
}
