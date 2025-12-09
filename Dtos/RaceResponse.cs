using f1api.Models;

namespace f1api.Dtos
{
    public class RaceResponse
    {
       public int Id { get; set; }
        public required string RaceName { get; set; }

        public DateTime RaceDate { get; set; }

        public string? Winner { get; set; }

        public string? Second { get; set; }

        public string? Third { get; set; }
    }
}
