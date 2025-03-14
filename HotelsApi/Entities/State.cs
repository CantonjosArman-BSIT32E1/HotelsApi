using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelsApi.Entities
{
    public class State  
    {
        [Key]
        public int StateId { get; set; }

        public string StateCode { get; set; }

    
        public string StateName { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public ICollection<City> City { get; set; } = []; // Fix: Navigation property
    }
}