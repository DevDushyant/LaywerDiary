using AuditTrail.Abstrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.CaseDetails
{
    [Table("case_titles", Schema = "ld")]
    public class CaseTitleEntity : AuditableEntity
    {
        [Key]
        public new Guid Id { get; set; }
        public int TypeId { get; set; }
        public Guid CaseId { get; set; }        
        public List<CaseApplicantDetailEntity> CaseApplicants { get; set; }
        public virtual CaseDetailEntity Case { get; set; }
    }
    
    public class CaseApplicantDetailEntity
    {
        public int ApplicantNo { get; set; }
        public string ApplicantDetail { get; set; }
    }
}
