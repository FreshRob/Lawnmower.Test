﻿using Lawnmower.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lawnmower.Dto;

namespace Lawnmower.BusinessLogic
{
    public class LawnmowerCommander : ILawnmowerCommander
    {
        public LawnmowerPosition RunInstructions(LawnmowerInstructions lawnmowerInstructions)
        {
            var lawnmower = new LawnmowerPosition
            {
                Facing = lawnmowerInstructions.StartDirection,
                X = lawnmowerInstructions.StartX,
                Y = lawnmowerInstructions.StartY
            };

           foreach(var instruction in lawnmowerInstructions.Instructions)
            {
                lawnmower.Facing = GetNewPosition(lawnmower.Facing, instruction);
                lawnmower.X = getXPosition(lawnmower.X, lawnmowerInstructions.BoundaryWidth,lawnmower.Facing, instruction);
                lawnmower.Y = getYPosition(lawnmower.Y, lawnmowerInstructions.BoundaryHeight, lawnmower.Facing, instruction);
            }

            return lawnmower;
        }

        private int getYPosition(int current, int limit, string facing, char instruction)
        {
            if (instruction != 'M')
            {
                return current;
            }

            switch (facing)
            {
                case "S":
                    return getPositionAfterNegativeMove(current);
                case "N":
                    return getPositionAfterPositiveMove(limit, current);
                default:
                    return current;
            }
        }

        private int getXPosition(int current, int limit, string facing, char instruction)
        {
            if (instruction != 'M')
            {
                return current;
            }

            switch (facing)
            {
                case "W":
                    return getPositionAfterNegativeMove(current);
                case "E":
                    return getPositionAfterPositiveMove(limit, current);
                default:
                    return current;
            }
        }

        private int getPositionAfterNegativeMove(int current)
        {
            var newPosition = current - 1;
            return (newPosition < 0) ? current : newPosition;
        }

        private int getPositionAfterPositiveMove(int limit, int current)
        {
            var newPosition = current + 1;
            return (newPosition > limit) ? current : newPosition;
        }



        private string GetNewPosition(string position, char positionAction)
        {
           if(positionAction == 'M')
            {
                return position;
            }

            switch (position)
            {
                case "N":
                    switch (positionAction)
                    {
                        case 'L':
                            return "W";
                        case 'R':
                            return "E";
                        default:
                            return position;
                    }
                case "E":
                    switch (positionAction)
                    {
                        case 'L':
                            return "N";
                        case 'R':
                            return "S";
                        default:
                            return position;
                    }
                case "S":
                    switch (positionAction)
                    {
                        case 'L':
                            return "E";
                        case 'R':
                            return "W";
                        default:
                            return position;
                    }
                case "W":
                    switch (positionAction)
                    {
                        case 'L':
                            return "S";
                        case 'R':
                            return "N";
                        default:
                            return position;
                    }
            }

            return position;
        }
    }
}
