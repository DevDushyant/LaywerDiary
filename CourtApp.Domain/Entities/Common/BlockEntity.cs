using AuditTrail.Abstrations;
using CourtApp.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Common
{
    [Table("m_block")]
    [Index(nameof(Code), IsUnique = true)]
    public class BlockEntity : AuditableEntity
    {
        public int Code { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
        [ForeignKey("DistrictCode")]
        public virtual DistrictEntity District { get; set; }
    }
}
