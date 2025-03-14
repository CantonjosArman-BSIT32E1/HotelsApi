using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelsApi.Entities
{
    public class Hotels
    {
        [Key]
        public int? HotelId { get; set; }

        public string HotelCode { get; set; }

        public string HotelName { get; set; }

        public string HotelDescription { get; set; }

        [ForeignKey("Barangay")]
        public int BarangayId { get; set; }
        public virtual Barangay Barangay { get; set; }


        public ICollection<Transaction> Transactions { get; set; } = []; // Fix: Navigation property



    }
}
