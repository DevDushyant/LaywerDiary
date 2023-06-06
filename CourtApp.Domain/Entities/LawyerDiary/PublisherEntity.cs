using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.LawyerDiary
{
      [Table("Mst_Publisher",Schema ="LDiary")]
    public class PublisherEntity : AuditableEntity
    {
        [Required]
        public string PublicationName { get; set; }        
        public string PropriatorName { get; set; }
    }
}