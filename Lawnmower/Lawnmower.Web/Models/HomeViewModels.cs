using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lawnmower.Web.Models
{
    public class HomeViewModel
    {
        [Required]
        public string LawnmowersInstructions { get; set; }

        public IList<Dto.LawnmowerPosition> LawnmowerPositions { get; set; }

        public HomeViewModel()
        {
            LawnmowerPositions = new List<Dto.LawnmowerPosition>();
        }
    }
}
