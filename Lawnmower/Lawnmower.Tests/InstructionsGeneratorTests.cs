using Lawnmower.BusinessLogic;
using Lawnmower.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lawnmower.Tests
{
    [TestClass]
    public class InstructionsGeneratorTests
    {
        private IInstructionsGenerator instructionsGenerator;

        [TestInitialize]
        public void Setup()
        {
            instructionsGenerator = new InstructionsGenerator();
        }

        [TestMethod]
        public void GetInstructions_emptyInstructions_ThrowException()
        {
            try
            {
                instructionsGenerator.GetInstructions("");
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void GetInstructions_TwoInstructions_ThrowException()
        {
            var instruction = "5 5" + Environment.NewLine + "1 2 N";

            try
            {
                instructionsGenerator.GetInstructions(instruction);
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void GetInstructions_SingleMowerInstruction_1Instruction()
        {
            var instruction = "5 7" + Environment.NewLine + "1 2 N" + Environment.NewLine + "LMLMLMM";

            var result = instructionsGenerator.GetInstructions(instruction);
            var firstResult = result.First();

            Assert.AreEqual(5, firstResult.BoundaryWidth);
            Assert.AreEqual(7, firstResult.BoundaryHeight);
            Assert.AreEqual(1, firstResult.StartX);
            Assert.AreEqual(2, firstResult.StartY);
            Assert.AreEqual("N", firstResult.StartDirection);
            Assert.AreEqual("LMLMLMM", string.Concat(firstResult.Instructions));
        }

        [TestMethod]
        public void GetInstructions_TwpMowerInstructions_2Instructions()
        {
            var instruction = "5 7" + Environment.NewLine + "1 2 N" + Environment.NewLine + "LMLMLMM"
                + Environment.NewLine + "2 5 E" + Environment.NewLine + "LM";

            var result = instructionsGenerator.GetInstructions(instruction);
            var secondResult = result[1];

            Assert.AreEqual(5, secondResult.BoundaryWidth);
            Assert.AreEqual(7, secondResult.BoundaryHeight);
            Assert.AreEqual(2, secondResult.StartX);
            Assert.AreEqual(5, secondResult.StartY);
            Assert.AreEqual("E", secondResult.StartDirection);
            Assert.AreEqual("LM", string.Concat(secondResult.Instructions));
        }
    }
}
