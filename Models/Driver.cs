namespace f1api.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;

        public int RacingNumber { get; set; }

        public int? TeamId { get; set; }
        public Team Team { get; set; } 

        public string Description { get; set; } = string.Empty;
    }
}
