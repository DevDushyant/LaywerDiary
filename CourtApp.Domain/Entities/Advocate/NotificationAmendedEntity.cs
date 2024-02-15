using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.Advocate
{
    [Table("Mst_AmendedNotification")]
    public class NotificationAmendedEntity : BaseEntity
    {
        [ForeignKey("Notification_Amended")]
        public int NotificationId { get; set; }
        public int AmendedNotificationID { get; set; }
    }
}
