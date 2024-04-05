using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.LawyerDiary
{
    [Table("case_detail", Schema = "ld")]
    public class CaseEntity : AuditableEntity
    {
        #region Properties without navigational
        public new Guid Id { get; set; }
        public required DateTime InstitutionDate { get; set; }
        public required Guid NatureId { get; set; }
        public required Guid CaseTypeId { get; set; }
        public required Guid CourtTypeId { get; set; }
        public required Guid CourtId { get; set; }
        public required Guid CaseKindId { get; set; }
        public required string Number { get; set; }
        public required int Year { get; set; }
        public string CisNumber { get; set; }
        public int CisYear { get; set; }
        public string CnrNumber { get; set; }
        public required string FirstTitle { get; set; }
        public required int FirstTitleCode { get; set; }
        public required string SecondTitle { get; set; }
        public required int SecoundTitleCode { get; set; }
        public DateTime NextDate { get; set; }
        public string CaseStageCode { get; set; }       
        public Guid LinkedCaseId { get; set; }
        public Guid ClientId { get; set; }
        public virtual NatureEntity Nature { get; set; }
        public virtual TypeOfCasesEntity TypeOfCase { get; set; }
        public virtual CourtTypeEntity CourtType { get; set; }
        public virtual CourtMasterEntity Court { get; set; }        
        public virtual CaseKindEntity CaseKind { get; set; }        
            
        #endregion



        //public new Guid Id { get; set; }
        //public required DateTime InstitutionDate { get; set; }

        //[ForeignKey("CaseNature")]
        //public required Guid NatureId { get; set; }
        //public virtual NatureEntity CaseNature { get; set; }

        //[ForeignKey("TypeOfCase")]
        //public required Guid TypeCaseId { get; set; }
        //public  virtual TypeOfCasesEntity TypeOfCase { get; set; }

        //[ForeignKey("CourtType")]
        //public required Guid CourtTypeId { get; set; }
        //public virtual CourtTypeEntity CourtType { get; set; }

        //[ForeignKey("Court")]
        //public required Guid CourtId { get; set; }
        //public virtual CourtMasterEntity Court { get; set; }

        //[ForeignKey("CaseType")]
        //public required Guid CaseTypeId { get; set; }
        //public CaseKindEntity CaseType { get; set; }
        //public required string Number { get; set; }
        //public required int Year { get; set; }
        //public string CisNumber { get; set; }
        //public int CisYear { get; set; }
        //public string CnrNumber { get; set; }
        //public required string FirstTitle { get; set; }
        //public required int TitleTypeFirst { get; set; }
        //public required string SecondTitle { get; set; }
        //public required int TitleTypeSecond { get; set; }
        //public DateTime NextDate { get; set; }
        //public string CaseStageCode { get; set; }
        //public Guid LinkedCaseId { get; set; }

        //[ForeignKey("Client")]
        //public Guid ClientId { get; set; }
        //public virtual ClientEntity Client { get; set; } = null;
        //public ICollection<AgainstCaseDetails> AgainstCaseDetails { get; set; } = null;
    }
}
