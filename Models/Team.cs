namespace f1api.Models
{
    public class Team
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public required string Principal { get; set; }

        public ICollection<Driver> Drivers { get; set; } = new List<Driver>();
    }
}
