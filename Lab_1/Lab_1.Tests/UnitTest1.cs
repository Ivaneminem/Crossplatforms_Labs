using System;
using System.Collections.Generic;
using NUnit.Framework;
using Lab_1;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Lab_1.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test_MaxRewardCalculation_WithValidData()
        {
            var orders = new List<(int Deadline, int Reward)>
            {
                (1, 10),
                (2, 12)
            };

            int expectedReward = 22;

            int actualReward = Program.CalculateMaxReward(orders);

            TestContext.WriteLine($"Expected Reward: {expectedReward}, Actual Reward: {actualReward}");

            Assert.AreEqual(expectedReward, actualReward, "The reward calculation is not as expected.");
        }

        [Test]
        public void Test_MaxRewardCalculation_WithOverlappingDeadlines()
        {
            var orders = new List<(int Deadline, int Reward)>
            {
                (1, 10),
                (1, 20),
                (3, 24)
            };

            int expectedReward = 44;

            int actualReward = Program.CalculateMaxReward(orders);

            TestContext.WriteLine($"Expected Reward: {expectedReward}, Actual Reward: {actualReward}");

            Assert.AreEqual(expectedReward, actualReward, "The reward calculation is not as expected.");
        }

        [Test]
        public void Test_MaxRewardCalculation_WithInvalidData()
        {
            var orders = new List<(int Deadline, int Reward)>
            {
                (1001, 50)
            };

            var ex = Assert.Throws<ArgumentException>(() => Program.CalculateMaxReward(orders));
            Assert.AreEqual("Некоректний дедлайн замовлення. Максимально допустиме значення - 1000.", ex.Message);

            TestContext.WriteLine("Check of incorrect data was successful.");
        }

        [Test]
        public void Test_MaxRewardCalculation_WithEmptyList()
        {
            var orders = new List<(int Deadline, int Reward)>();

            int expectedReward = 0;

            int actualReward = Program.CalculateMaxReward(orders);

            TestContext.WriteLine("Test with an empty list of orders. Expected reward: 0");

            Assert.AreEqual(expectedReward, actualReward, "The calculation for an empty list is not as expected.");
        }

        [Test]
        public void Test_MaxRewardCalculation_WithZeroReward()
        {
            var orders = new List<(int Deadline, int Reward)>
            {
                (1, 0),
                (2, 0),
                (3, 0)
            };

            int expectedReward = 0;

            int actualReward = Program.CalculateMaxReward(orders);

            TestContext.WriteLine("Zero reward test for all orders. Expected reward: 0");

            Assert.AreEqual(expectedReward, actualReward, "The calculation for orders with zero reward is not as expected.");
        }

    }
}
