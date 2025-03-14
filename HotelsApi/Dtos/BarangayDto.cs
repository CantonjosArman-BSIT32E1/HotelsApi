using System.ComponentModel.DataAnnotations;

namespace HotelsApi.Dtos
{
    public class CreateBarangayModel
    {

        public string BarangayName { get; set; } = null!;


        public string PostalCode { get; set; } = null!;


        public int? CityId { get; set; }
    }

    public class UpdateBarangayModel : CreateBarangayModel
    {

        public int BarangayId { get; set; }
    }

    public class GetBarangayModel : UpdateBarangayModel
    {
    }
}
