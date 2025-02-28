using System.ComponentModel.DataAnnotations;

namespace HotelsApi.Entities
{
    public class State  
    {
        [Key]
        public int StateId { get; set; }

        public string? StateCode { get; set; }

        [Required]
        public string StateName { get; set; }
    }
}