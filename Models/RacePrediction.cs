using System.ComponentModel.DataAnnotations.Schema;

namespace f1api.Models
{
    public class RacePrediction
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public int RaceId { get; set; }
        public Race Race { get; set; }

        public int WinnerId { get; set; }
        [ForeignKey("WinnerId")]
        public Driver Winner { get; set; }
        public int SecondId { get; set; }
        [ForeignKey("SecondId")]
        public Driver Second { get; set; }
        public int ThirdId { get; set; }
        [ForeignKey("ThirdId")]
        public Driver Third { get; set; }

        public DateTime? PredictionTime { get; set; } = DateTime.UtcNow;

        public bool IsWinner { get; set; } = false;
    }
}
