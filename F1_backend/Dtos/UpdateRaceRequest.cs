using f1api.Models;

namespace f1api.Dtos
{
    public class UpdateRaceRequest
    {
        public int Id { get; set; }
        public int? WinnerId { get; set; }
        

        public int? SecondId { get; set; }
      


        public int? ThirdId { get; set; }
        
    }
}
