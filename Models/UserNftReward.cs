namespace f1api.Models
{
    public class UserNftReward
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public int RaceId {get; set; }
        public Race Race { get; set; }
        public string NFTHash { get; set; }
        public string NftImageUrl { get; set; }
        public DateTime AwardedDate { get; set; } = DateTime.UtcNow;
    }
}
