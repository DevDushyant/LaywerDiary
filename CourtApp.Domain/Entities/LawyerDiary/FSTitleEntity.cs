using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_fs_title", Schema = "ld")]
    public class FSTitleEntity : AuditableEntity
    {        
        public new Guid Id { get; set; }
        public int TypeId { get; set; }
        public string Name_En { get; set; }
        public string Name_Hn { get; set; }
    }
}
