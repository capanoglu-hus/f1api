using f1api.Models;

namespace f1api.Dtos
{
    public class UpdateTeamRequest
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public required string Principal { get; set; }


    }
}
