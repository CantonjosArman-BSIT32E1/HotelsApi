using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelsApi.Entities
{
    public class Barangay
    {
            [Key]
            public int BarangayId { get; set; }

            [Required]
            public string? BarangayName { get; set; }

            [Required]
            public string? PostalCode { get; set; }

            [ForeignKey("City")]
            public int CityId { get; set; }
            public virtual City? City { get; set; }

    }
}
