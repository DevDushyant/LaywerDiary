using CourtApp.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using AuditTrail.Abstrations;
using System.Collections.Generic;

namespace CourtApp.Domain.Entities.Common
{
    [Table("m_gp")]
    [Index(nameof(Code), IsUnique = true)]
    public class GPEntity : AuditableEntity
    {
        public int Code { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        [ForeignKey("BlockCode")]
        public virtual BlockEntity Block { get; set; }
        public ICollection<VillageEntity> Villages { get; set; }
    }
}
