using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace f1api.Models
{
    public class UserDriverVote
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public int FirstDriverId { get; set; }
        [ForeignKey("FirstDriverId")]
        public Driver FirstDriver { get; set; }

        public int SecondDriverId { get; set; }
        [ForeignKey("SecondDriverId")]
        public Driver SecondDriver { get; set; }

        public int ThirdDriverId { get; set; }
        [ForeignKey("ThirdDriverId")]
        public Driver ThirdDriver { get; set; }

        public DateTime VoteDate { get; set; } = DateTime.UtcNow;
    }
}
