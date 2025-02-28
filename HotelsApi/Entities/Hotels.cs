using System.ComponentModel.DataAnnotations;

namespace HotelsApi.Entities
{
    public class Hotels
    {
            [Key]
            public int HotelId { get; set; }

            [Required]
            public string HotelCode { get; set; }

            [Required]
            public string HotelName { get; set; }

            [Required]
            public string HotelDescription { get; set; }
        
    }
}
