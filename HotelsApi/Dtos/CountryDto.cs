namespace HotelsApi.Dtos
{
    public class CreateCountryModel
    {
        public string CountryName { get; set; } = null!;
        public string CountryCode { get; set; } = null!;
    }

    public class UpdateCountryModel : CreateCountryModel
    {
        public int CountryId { get; set; }
    }

    public class GetCountryModel : UpdateCountryModel
    {
    }
}
