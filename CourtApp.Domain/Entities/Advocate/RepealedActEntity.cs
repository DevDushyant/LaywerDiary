using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.Advocate
{
    [Table("Mst_RepealedAct")]
    public class RepealedActEntity : BaseEntity
    {
        [ForeignKey("Act")]
        public int ActID { get; set; }        
        public int RepealedActID { get; set; }
        public ActEntity Act { get; set; }
    }
}
