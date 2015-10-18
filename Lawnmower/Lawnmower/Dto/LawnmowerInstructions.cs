using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lawnmower.Dto
{
    public class LawnmowerInstructions
    {
        public int BoundaryWidth { get; set; }
        public int BoundaryHeight { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        
        public string StartDirection { get; set; }
        public IList<char> Instructions { get; set; }

        public LawnmowerInstructions()
        {
            Instructions = new List<char>();
        }
    }
}
