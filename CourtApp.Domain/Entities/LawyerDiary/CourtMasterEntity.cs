using AspNetCoreHero.Abstractions.Domain;
using CourtApp.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("Mst_Court",Schema ="LDiary")]
    public class CourtMasterEntity : AuditableEntity
    {
        
        public Guid UniqueId { get; set; }
        public string CourtName { get; set; }
        public string Bench { get; set; }
        public string HeadQuerter { get; set; }
        public string Address { get; set; }
       
        [Required]
        [ForeignKey("District")]
        public int DistrictCode { get; set; }
        public virtual DistrictEntity District { get; set; }

        [Required]
        [ForeignKey("State")]
        public string StateCode { get; set; }
        public virtual StateEntity State { get; set; }

        [Required]
        [ForeignKey("CourtType")]
        public int CourtTypeId { get; set; }
        public virtual CourtTypeEntity CourtType { get; set; }
    }
}