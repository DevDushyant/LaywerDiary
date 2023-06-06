using System.ComponentModel.DataAnnotations.Schema;
using AspNetCoreHero.Abstractions.Domain;

namespace CourtApp.Entities.Common
{
    [Table("Mst_Country",Schema = "Common")]
    public class CountryEntity
    {
       
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }
}