namespace f1api.Dtos
{
    public class CreateRaceRequest
    {
        public int Id { get; set; }
        public string RaceName { get; set; }

        public DateTime RaceDate { get; set; }
    }
}
