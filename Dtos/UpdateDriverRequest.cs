namespace f1api.Dtos
{
    public class UpdateDriverRequest
    {
       // public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Team { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
