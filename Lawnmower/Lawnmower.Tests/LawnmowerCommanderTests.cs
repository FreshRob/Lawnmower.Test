using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lawnmower.Interfaces;
using Lawnmower.BusinessLogic;
using Lawnmower.Dto;
using System.Collections.Generic;

namespace Lawnmower.Tests
{
    [TestClass]
    public class LawnmowerCommanderTests
    {
        private ILawnmowerCommander lawnmowerCommaner;

        [TestInitialize]
        public void Setup()
        {
            lawnmowerCommaner = new LawnmowerCommander();
        }

        [TestMethod]
        public void RunInstructions_Move_MoveNorthOnce()
        {
            var instructions = new LawnmowerInstructions
            {
                BoundaryWidth = 5,
                BoundaryHeight = 5,
                StartX = 1,
                StartY = 2,
                StartDirection = "N",
                Instructions = new List<char>
                {
                    'L',
                    'M',
                    'L',
                    'M',
                    'L',
                    'M',
                    'L',
                    'M',
                    'M'
                }
            };

            var result = lawnmowerCommaner.RunInstructions(instructions);

            Assert.AreEqual(1, result.X);
            Assert.AreEqual(3, result.Y);
            Assert.AreEqual("N", result.Facing);
        }

        [TestMethod]
        public void RunInstructions_Move_faceNorth()
        {
            var instructions = new LawnmowerInstructions
            {
                BoundaryWidth = 5,
                BoundaryHeight = 5,
                StartX = 3,
                StartY = 3,
                StartDirection = "E",
                Instructions = new List<char>
                {
                    'M',
                    'M',
                    'R',
                    'M',
                    'M',
                    'R',
                    'M',
                    'R',
                    'R',
                    'M'
                }
            };

            var result = lawnmowerCommaner.RunInstructions(instructions);

            Assert.AreEqual(5, result.X);
            Assert.AreEqual(1, result.Y);
            Assert.AreEqual("E", result.Facing);
        }
    }
}
