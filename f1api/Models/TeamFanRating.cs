using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace f1api.Models
{
    public class TeamFanRating
    {
        [Key]
        public int Id { get; set; }
        public int TeamId { get; set; }
        [ForeignKey("TeamId")]
        public Team Team { get; set; }
        public int TotalVotes { get; set; } = 0;
        public DateTime RatedDate { get; set; } = DateTime.UtcNow;
    }
}
