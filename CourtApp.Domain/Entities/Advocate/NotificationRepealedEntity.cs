using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.Advocate
{
    [Table("Mst_RepealedNotification")]
    public class NotificationRepealedEntity : BaseEntity
    {
        [ForeignKey("Notification")]
        public int NotificationId { get; set; }
        public int RepealedNotificationID { get; set; }
    }
}
