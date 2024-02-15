using AuditTrail.Abstrations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_expense_head", Schema = "ld")]
    public class ExpenseHeadEntity : AuditableEntity
    {
        [Required]
        public string HeadName { get; set; }
    }
}