namespace HotelsApi.Dtos
{
    public class CreateCityModel
    {
        public string CityName { get; set; } = null!;
        public string CityCode { get; set; } = null!;
        public int StateId { get; set; }
    }

    public class UpdateCityModel : CreateCityModel
    {
        public int CityId { get; set; }
    }

    public class GetCityModel : UpdateCityModel
    {
    }
}
