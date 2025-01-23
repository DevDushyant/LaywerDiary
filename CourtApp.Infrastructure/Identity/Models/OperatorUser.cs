using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Identity.Models
{
    [Table("operator", Schema = "Identity")]
    public class OperatorUser:AuditableEntity
    {       
        /// <summary>
        /// Foreign  key to applicaition user.
        /// </summary>
        public string LawyerId { get; set; }
        public ApplicationUser Lawyer { get; set; }
        public DateTime DateOfJoining { get; set; }
        public AddressInfo AddressInfo { get; set; }

    }
}
