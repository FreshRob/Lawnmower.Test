using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lawnmower.Interfaces
{
    public interface IInstructionsGenerator
    {
        IList<Dto.LawnmowerInstructions> GetInstructions(string lineByLineInstructions);
    }
}
