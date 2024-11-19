using Lab_3;
using System;
using NUnit.Framework;

namespace Lab_3.Tests
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Console.WriteLine("Starting a new test...");
        }

        [TearDown]
        public void Teardown()
        {
            Console.WriteLine("Test completed.\n");
        }

        [Test]
        public void Test_ValidMatrix_SingleNonZeroSource()
        {
            Console.WriteLine("Test 1: Simple case with a single source of non-zero values.");
            int[,] input = {
                { 0, 0, 0 },
                { 0, 5, 0 },
                { 0, 0, 0 }
            };

            int[,] expected = {
                { 5, 5, 5 },
                { 5, 5, 5 },
                { 5, 5, 5 }
            };

            int[,] result = Program.ReplaceZerosWithClosestNonZero(input, 3);

            PrintMatrix(result); 
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Test_ValidMatrix_WithMultipleClosestValues()
        {
            Console.WriteLine("Test 2: Several equally spaced values.");
            int[,] input = {
            { 0, 1, 0 },
            { 2, 0, 3 },
            { 0, 4, 0 }
        };

            int[,] expected = {
            { 0, 1, 0 },
            { 2, 0, 3 },
            { 0, 4, 0 }
        }; 

            int[,] result = Program.ReplaceZerosWithClosestNonZero(input, 3);

            PrintMatrix(result);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Test_ValidMatrix_EdgeCase()
        {
            Console.WriteLine("Test 3: Matrix without zeros.");
            int[,] input = {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };

            int[,] expected = {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        }; 

            int[,] result = Program.ReplaceZerosWithClosestNonZero(input, 3);

            PrintMatrix(result);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Test_InvalidMatrix_WithNegativeValues()
        {
            Console.WriteLine("Test 4: Incorrect data — negative values.");
            int[,] input = {
            { -1, 2, 3 },
            { 4, -5, 6 },
            { 7, 8, 9 }
        };

            var ex = Assert.Throws<ArgumentException>(() => Program.ReplaceZerosWithClosestNonZero(input, 3));
            Console.WriteLine($"Error message received: {ex.Message}");
            Assert.AreEqual("Matrix elements must be non-negative integers.", ex.Message);
        }

        [Test]
        public void Test_EmptyMatrix()
        {
            Console.WriteLine("Test 5: Empty matrix (size 0x0).");
            int[,] input = new int[0, 0];

            int[,] result = Program.ReplaceZerosWithClosestNonZero(input, 0);

            PrintMatrix(result);
            Assert.AreEqual(new int[0, 0], result);
        }

        // Допоміжний метод для виведення матриці
        private void PrintMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            Console.WriteLine("Result matrix:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}