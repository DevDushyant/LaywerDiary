﻿using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.Advocate
{
    [Table("ad.rule_book")]
    public class RuleBookEntity : AuditableEntity
    {
        [ForeignKey("Rule")]
        public int RuleId { get; set; }
        public int BookId { get; set; }
        public int BookYear { get; set; }
        public string BookPageNo { get; set; }
        public int? BookSrNo { get; set; }
        public string Volume { get; set; }
    }
}
