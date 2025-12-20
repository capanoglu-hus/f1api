using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace f1api.Models
{
    public class DriverFanRating
    {
        [Key]
        public int Id { get; set; }
        public int DriverId { get; set; }
        [ForeignKey("DriverId")]
        public Driver Driver { get; set; }
        public int TotalScore { get; set; } = 0;
        public int TotalVotes { get; set; } = 0;
        public DateTime RatedDate { get; set; } = DateTime.UtcNow;
    }
}
