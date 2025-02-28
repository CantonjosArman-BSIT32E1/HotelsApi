using System.ComponentModel.DataAnnotations;

namespace HotelsApi.Entities
{
    public class City
    {

            [Key]
            public int CityId { get; set; }

            public string? CityCode { get; set; }

            [Required]
            public string CityName { get; set; }
        
    }
}
