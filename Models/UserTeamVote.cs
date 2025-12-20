namespace f1api.Models
{
    public class UserTeamVote
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        
        public DateTime VoteDate { get; set; } = DateTime.UtcNow;
    }
}
