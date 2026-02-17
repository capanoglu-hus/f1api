using f1api.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace f1api.Dtos
{
    public class DriverVote
    {
      
        
      
        public int FirstDriverId { get; set; }
        

        public int SecondDriverId { get; set; }
       

        public int ThirdDriverId { get; set; }
       
    }
}
