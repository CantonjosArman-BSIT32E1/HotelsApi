namespace HotelsApi.Dtos
{
    public class CreateTransactionModel
    {
        public int? HotelId { get; set; }
        public string HotelName { get; set; } 
        public string HotelCode { get; set; }
        public DateOnly DateFrom { get; set; }
        public DateOnly DateTo { get; set; }
        public string FullName { get; set; } 
        public string PhoneNumber { get; set; } 
        public string EmailAddress { get; set; } 
    }

    public class UpdateTransactionModel : CreateTransactionModel
    {
        public int TransactionId { get; set; }
    }

    public class GetTransactionModel : UpdateTransactionModel
    {
    }
}
