using f1api.Models;

namespace f1api.Dtos
{
    public class TeamResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public required string Principal { get; set; }

        public List<CreateDriverRequest> Drivers { get; set; } 

    }
}
