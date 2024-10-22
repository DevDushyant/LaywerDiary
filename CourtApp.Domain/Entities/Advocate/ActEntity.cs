using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Advocate
{
    [Table("m_act", Schema = "ad")]
    public class ActEntity : AuditableEntity
    {
        public string ActCategory { get; set; }
        public required int ActNumber { get; set; }
        public required int SubActNumber { get; set; }
        public int ActYear { get; set; }
        public string AssentBy { get; set; }
        public DateTime? AssentDate { get; set; }
        public string ActName { get; set; }
        public int GazetteId { get; set; }
        public string Nature { get; set; }
        public DateTime? GazetteDate { get; set; }
        public int? PageNo { get; set; }
        public string ComeInforce { get; set; }
        public DateTime? PublishedGazeteDate { get; set; }
        public Guid SubjectId { get; set; }
        public SubjectEntity Subject { get; set; }
        public Guid ActTypeId { get; set; }
        public ActTypeEntity ActType { get; set; }
        public Guid PartId { get; set; }
        public PartEntity Part { get; set; }
        public virtual ICollection<ActAmendedEntity> AmendedActs { get; set; }
        public virtual ICollection<ActRepealedEntity> RepealedActs { get; set; }
        public virtual ICollection<ActBookEntity> ActBooks { get; set; }
    }
}
