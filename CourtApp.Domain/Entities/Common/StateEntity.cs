using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtApp.Entities.Common
{
    [Table("Mst_State",Schema = "Common")]
    public class StateEntity 
    {
        
        public string StateCode { get; set; }
        public String StateName { get; set; }      

        [ForeignKey("Country")]
        public string CountryCode { get; set; }
        public virtual CountryEntity Country { get; set; }
    }
}