﻿using AuditTrail.Abstrations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Domain.Entities.Advocate
{
	[Table("ad.m_egazzet")]
	public class EGazzetDataEntity : AuditableEntity
    {		
		public int gazzetTypeId { get; set; }
		public string oraganization { get; set; }
		public string department { get; set; }
		public string office { get; set; }
		public string subject { get; set; }
		public string category { get; set; }
		public string part_section { get; set; }
		public string issue_date { get; set; }
		public string publish_date { get; set; }
		public string reference_no { get; set; }
		public string file_size { get; set; }
		public string file_name { get; set; }
	}

}
