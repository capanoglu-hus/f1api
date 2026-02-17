using f1api.Models;

namespace f1api.Dtos
{
    public class CreateTeamRequest
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public required string Principal { get; set; }

       

    }
}
