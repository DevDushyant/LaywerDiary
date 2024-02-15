using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.Advocate
{
    [Table("Mst_RepealedRule")]
    public class RuleRepealedEntity : BaseEntity
    {
        [ForeignKey("Rule")]
        public int RuleId { get; set; }
        public int RepealedRuleID { get; set; }
    }
}
