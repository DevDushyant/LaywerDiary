using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.Advocate
{
    [Table("Mst_AmendedRule")]
    public class RuleAmendedEntity : BaseEntity
    {
        [ForeignKey("Rule")]
        public int RuleId { get; set; }
        public int AmendedRuleID { get; set; }
    }
}
