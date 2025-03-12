using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelsApi.Entities;

public partial class Transactions
{
    [Key]
    public int TransactionId { get; set; }

    [ForeignKey("HotelId")]
    public int HotelId { get; set; }

    public string HotelName { get; set; } = null!;
    public string HotelCode { get; set; } = null!;
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string EmailAddress { get; set; } = null!;

    public virtual Hotels? Hotels { get; set; }
}
