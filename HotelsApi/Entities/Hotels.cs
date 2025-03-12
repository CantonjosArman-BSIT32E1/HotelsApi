﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelsApi.Entities
{
    public class Hotels
    {
            [Key]
            public int HotelId { get; set; }

            [Required]
            public string? HotelCode { get; set; }

            [Required]
            public string? HotelName { get; set; }

            [Required]
            public string? HotelDescription { get; set; }

            [ForeignKey("Barangay")]
            public int BarangayId { get; set; }
            public virtual Barangay? Barangay { get; set; }

    }
}
