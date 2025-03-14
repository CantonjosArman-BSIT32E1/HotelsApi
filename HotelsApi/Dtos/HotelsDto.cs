using System.ComponentModel.DataAnnotations;

namespace HotelsApi.Dtos
{
    public class CreateHotelModel
    {

        public string HotelName { get; set; } 


        public string HotelCode { get; set; } 

        public string HotelDescription { get; set; } 


        public int BarangayId { get; set; }
    }

    public class UpdateHotelModel : CreateHotelModel
    {
        public int HotelId { get; set; }
    }

    public class GetHotelModel : UpdateHotelModel
    {
    }
}
    