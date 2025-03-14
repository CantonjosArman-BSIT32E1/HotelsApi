namespace HotelsApi.Dtos
{
    public class CreateStateModel
    {
        public string StateName { get; set; } = null!;
        public string StateCode { get; set; } = null!;
        public int CountryId { get; set; }
    }

    public class UpdateStateModel : CreateStateModel
    {
        public int StateId { get; set; }
    }

    public class GetStateModel : UpdateStateModel
    {
    }
}
