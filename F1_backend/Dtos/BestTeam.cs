using f1api.Models;

namespace f1api.Dtos
{
    public class BestTeam
    {
        /*
         {
  "id": 2,
  "name": "Mercedes-AMG Petronas",
  "principal": "Toto Wolff",
  "drivers": []
}*/
        public  int TeamId { get; set; }
        public string TeamName { get; set; }
        public string TeamPrincipal { get; set; }
        public List<CreateDriverRequest> Drivers { get; set; }
        public int TotalVotes { get; set; }
        public DateTime VoteUpdateDate { get; set; }
    }
}
