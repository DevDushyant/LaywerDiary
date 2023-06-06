using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("Mst_ExpenseHead", Schema = "LDiary")]
    public class ExpenseHeadEntity : AuditableEntity
    {
        [Required]
        public string HeadName { get; set; }
    }
}