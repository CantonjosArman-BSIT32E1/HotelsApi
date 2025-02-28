using System.ComponentModel.DataAnnotations;

namespace HotelsApi.Entities
{
    public class Barangay
    {
            [Key]
            public int BarangayId { get; set; }

            [Required]
            public string BarangayName { get; set; }

            [Required]
            public string PostalCode { get; set; }
        
    }
}
