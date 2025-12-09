namespace f1api.Models
{
    public class Race
    {
        public int Id { get; set; }
        public required string RaceName { get; set; }

        public DateTime RaceDate { get; set; }


        public int? WinnerId { get; set; }
        public Driver? Winner { get; set; }

        public int? SecondId { get; set; }
        public Driver? Second { get; set; }


        public int? ThirdId { get; set; }
        public Driver? Third { get; set; }
    }
}
