using Lawnmower.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lawnmower.Interfaces
{
    public interface ILawnmowerCommander
    {
        LawnmowerPosition RunInstructions(LawnmowerInstructions lawnmowerInstructions);
    }
}
