using f1api.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace f1api.Dtos
{
    public class BestDrivers
    {
        public int Id { get; set; }
     
        public string DriverName { get; set; }
        public int RacingNumber { get; set; }
        public string TeamName { get; set; }
        public string Description { get; set; }
        public int TotalScore { get; set; } 
        public int TotalVotes { get; set; } 
        public DateTime RatedDate { get; set; } 
    }
}
