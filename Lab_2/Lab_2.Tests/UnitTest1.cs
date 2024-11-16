using NUnit.Framework;
using System;
using Lab_2;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Lab_2.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void Test_SingleBlock_ReturnsZero()
        {
            var blocks = new[] { new Block(34, 35) };
            int result = Program.CalculateMinimumOperations(blocks);
            Console.WriteLine($"Test 1: Expected 0, got: {result}");
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Test_TwoBlocks_ReturnsCorrectValue()
        {
            var blocks = new[]
            {
                new Block(34, 35),
                new Block(35, 36)
            };
            int result = Program.CalculateMinimumOperations(blocks);
            Console.WriteLine($"Test 2: Expected 1190, got: {result}");
            Assert.AreEqual(34 * 36, result);
        }

        [Test]
        public void Test_ThreeBlocks_ReturnsCorrectValue()
        {
            var blocks = new[]
            {
                new Block(34, 35),
                new Block(35, 36),
                new Block(36, 37)
            };
            int result = Program.CalculateMinimumOperations(blocks);
            Console.WriteLine($"Test 3: Expected 2482, got: {result}");
            Assert.AreEqual(2482, result);
        }

        [Test]
        public void Test_InvalidBlockValues_ThrowsException()
        {
            var blocks = new[]
            {
                new Block(0, 1),
                new Block(101, 102)
            };

            var ex = Assert.Throws<ArgumentException>(() => Program.CalculateMinimumOperations(blocks));
            Console.WriteLine($"Test 4: Invalid input caused an error: {ex.Message}");
            Assert.That(ex.Message, Is.EqualTo("Block parameter values are out of allowed range."));
        }

        [Test]
        public void Test_EmptyBlocks_ThrowsException()
        {
            var blocks = Array.Empty<Block>();

            var ex = Assert.Throws<ArgumentException>(() => Program.CalculateMinimumOperations(blocks));
            Console.WriteLine($"Test 5: Empty block list caused an error: {ex.Message}");
            Assert.That(ex.Message, Is.EqualTo("Blocklist cannot be empty or null."));
        }
    }
}
