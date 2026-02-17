using f1api.Models;

namespace f1api.Dtos
{
    public class CreateDriverRequest
    {
        public string Name { get; set; } = string.Empty;

        
        public int RacingNumber { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
