using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelsApi.Entities
{
    public class City
    {

            [Key]
            public int? CityId { get; set; }

            public string CityCode { get; set; }

            public string CityName { get; set; }

            [ForeignKey("State")]
            public int StateId { get; set; }
            public virtual State State { get; set; }

            // Navigation property for Barangay
            public ICollection<Barangay> Barangay { get; set; } = []; // Fix: Navigation property
    }
}
