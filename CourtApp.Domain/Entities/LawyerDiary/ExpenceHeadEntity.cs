using AuditTrail.Abstrations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("m_expense_head", Schema = "ld")]
    public class ExpenseHeadEntity : AuditableEntity
    {
        public new Guid Id { get; set; }
        [Required]
        public string HeadName { get; set; }
    }
}