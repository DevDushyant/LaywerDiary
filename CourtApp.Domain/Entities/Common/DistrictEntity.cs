using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Entities.Common
{
    [Table("m_district")]
    [Index(nameof(Code), IsUnique = true)]
    public class DistrictEntity : AuditableEntity
    {
        public int Code { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }

        [ForeignKey("StateCode")]
        public virtual StateEntity State { get; set; }
        public ICollection<BlockEntity> Blocks { get; set; }
        public ICollection<CityEntity> Cities { get; set; }
    }
}