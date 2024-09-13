﻿using AuditTrail.Abstrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace CourtApp.Domain.Entities.FormBuilder
{
    [Table("m_temp_frm_mapping")]
    public class FormTemplateMappingEntity : AuditableEntity
    {
        public Guid TemplateId { get; set; }
        public Guid FormId { get; set; }
        public List<Mapping> FieldsMapping { get; set; }
    }
    public class Mapping
    {
        public string Tag { get; set; }
        public Guid Key { get; set; }
    }
}
