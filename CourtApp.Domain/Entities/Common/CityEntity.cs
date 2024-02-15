using CourtApp.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Domain.Entities.Common
{
    [Table("m_city")]
    public class CityEntity
    {
        [Key]
        public int CityCode { get; set; }
        public string CityName { get; set; }

        [ForeignKey("District")]
        public int DistrictCode { get; set; }
        public virtual DistrictEntity District { get; set; }
    }
}
