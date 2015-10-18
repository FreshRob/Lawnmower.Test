using Lawnmower.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lawnmower.Dto;
using System.IO;

namespace Lawnmower.BusinessLogic
{
    public class InstructionsGenerator : IInstructionsGenerator
    {
        public IList<LawnmowerInstructions> GetInstructions(string lineByLineInstructions)
        {
            if (string.IsNullOrWhiteSpace(lineByLineInstructions))
            {
                throw new ArgumentException();
            }

            var output = CreateInstructions(lineByLineInstructions);

            if (!output.Any())
            {
                throw new ArgumentException();
            }

            return output;
        }

        private List<LawnmowerInstructions> CreateInstructions(string rawInstructions)
        {
            int countLines = 0;
            var output = new List<LawnmowerInstructions>();
            var lawnmowerInstructionBoundary = (lawnmowerBoundaries) null;
            var tempLawnmowerInstruction = new LawnmowerInstructions();

            using (var reader = new StringReader(rawInstructions))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    countLines++;
                    var commands = line.Split(' ');

                    if (countLines == 1)
                    {
                        lawnmowerInstructionBoundary = GetBoundaries(commands);
                        if(lawnmowerInstructionBoundary == null)
                        {
                            return new List<LawnmowerInstructions>();
                        }
                        continue;
                    }

                    if (lineIsAboutLawnmowerPoistion(countLines))
                    {
                        tempLawnmowerInstruction = GetLawnmowerPosition(commands);
                        if(tempLawnmowerInstruction == null)
                        {
                            return new List<LawnmowerInstructions>();
                        }
                        continue;
                    }

                    AddMovementCommandsAndAddToOutputList(line, lawnmowerInstructionBoundary, tempLawnmowerInstruction, output);
                }
            }

            return output;
        }

        private class lawnmowerBoundaries
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        private LawnmowerInstructions GetLawnmowerPosition(string[] commands)
        {
            var startX = 0;
            var startY = 0;
            if (commands.Length != 3 ||
               !int.TryParse(commands[0], out startX) ||
               !int.TryParse(commands[1], out startY) ||
               !new string[] { "N", "E", "S", "W" }.Contains(commands[2]))
            {
                return null;
            }

            return new LawnmowerInstructions
            {
                StartX = startX,
                StartY = startY,
                StartDirection = commands[2]
            };
        }

        private lawnmowerBoundaries GetBoundaries(string[] commands)
        {
            var boundaryX = 0;
            var boundaryY = 0;

            if (commands.Length != 2 ||
                           !int.TryParse(commands[0], out boundaryX) ||
                           !int.TryParse(commands[1], out boundaryY))
            {
                return null;
            }

            return new lawnmowerBoundaries
            {
                X = boundaryX,
                Y = boundaryY
            };
        }


        private bool lineIsAboutLawnmowerPoistion(int countLines)
        {
            return countLines % 2 == 0;
        }


        private void AddMovementCommandsAndAddToOutputList(string commands, lawnmowerBoundaries lawnmowerInstructionBoundary, LawnmowerInstructions tempLawnmowerInstruction, List<LawnmowerInstructions> output)
        {
            tempLawnmowerInstruction.BoundaryWidth = lawnmowerInstructionBoundary.X;
            tempLawnmowerInstruction.BoundaryHeight = lawnmowerInstructionBoundary.Y;
            tempLawnmowerInstruction.Instructions = commands.ToCharArray();
            output.Add(tempLawnmowerInstruction);
        }
    }
}
