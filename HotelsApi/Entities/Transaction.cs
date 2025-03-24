using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelsApi.Entities;

public partial class Transaction
{
    [Key]
    public int TransactionId { get; set; }
    public string HotelName { get; set; } = null!;
    public string HotelCode { get; set; } = null!;
    public DateOnly DateFrom { get; set; }
    public DateOnly DateTo { get; set; }
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string EmailAddress { get; set; } = null!;

    [ForeignKey("HotelId")]
    public int? HotelId { get; set; }

    // Navigation property
    public virtual Hotels Hotel { get; set; }

}
