using System.ComponentModel.DataAnnotations;

namespace HotelsApi.Entities
{
    public class Country
    {
            [Key]
            public int? CountryId { get; set; }

         
            public string CountryCode { get; set; }

         
            public string CountryName { get; set; }

            public ICollection<State> State { get; set; } = []; // Fix: Navigation property


    }
}
