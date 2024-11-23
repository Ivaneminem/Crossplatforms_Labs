using Lab_2;
using System;
using System.IO;

namespace Lab_2
{
    public class Program
    {
        public const int MIN_INITIAL = 1;
        public const int MAX_INITIAL = 100;
        public const int MIN_FINAL = 1;
        public const int MAX_FINAL = 100;

        public static int CalculateMinimumOperations(Block[] blocks)
        {
            if (blocks == null || blocks.Length == 0)
                throw new ArgumentException("Blocklist cannot be empty or null.");

            if (blocks.Any(b => b.InitialValue < MIN_INITIAL || b.InitialValue > MAX_INITIAL ||
                                b.FinalValue < MIN_FINAL || b.FinalValue > MAX_FINAL))
            {
                throw new ArgumentException("Block parameter values are out of allowed range.");
            }

            if (blocks.Length == 1)
            {
                return 0;
            }

            int totalBlocks = blocks.Length;
            var initialValues = new int[totalBlocks + 1];
            var finalValues = new int[totalBlocks + 1];

            for (var idx = 0; idx < totalBlocks; idx++)
            {
                initialValues[idx + 1] = blocks[idx].InitialValue;
                finalValues[idx + 1] = blocks[idx].FinalValue;
            }

            var operationMatrix = new int[totalBlocks + 1, totalBlocks + 1];

            for (int segmentSize = 1; segmentSize <= totalBlocks; segmentSize++)
            {
                for (int startIdx = 1; startIdx + segmentSize - 1 <= totalBlocks; startIdx++)
                {
                    int endIdx = startIdx + segmentSize - 1;

                    if (segmentSize == 1)
                    {
                        operationMatrix[startIdx, endIdx] = 0;
                    }
                    else
                    {
                        int minOperations = int.MaxValue;

                        for (int splitIdx = startIdx; splitIdx < endIdx; splitIdx++)
                        {
                            int nextSegmentStart = splitIdx + 1;
                            int currentCost = operationMatrix[startIdx, splitIdx] + operationMatrix[nextSegmentStart, endIdx];
                            minOperations = Math.Min(minOperations, currentCost);
                        }

                        operationMatrix[startIdx, endIdx] = minOperations + initialValues[startIdx] * finalValues[endIdx];
                    }
                }
            }

            return operationMatrix[1, totalBlocks];
        }

        public static void Run(string inputFilePath, string outputFilePath)
        {
            // Зчитуємо вхідні дані з файлу INPUT.TXT
            var inputFile = File.ReadAllLines(inputFilePath);
            int totalElements = int.Parse(inputFile[0]);
            var blocks = new Block[totalElements];

            for (int index = 0; index < totalElements; index++)
            {
                var parts = inputFile[index + 1].Split(' ').Select(int.Parse).ToArray();
                blocks[index] = new Block(parts[0], parts[1]);
            }

            // Розв'язуємо задачу
            int result = CalculateMinimumOperations(blocks);
            // Крок 4: Запис результату у файл OUTPUT.TXT
            File.WriteAllText(outputFilePath, result.ToString());
        }

        static void Main()
        {
            string inputFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "INPUT.TXT");
            string outputFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "OUTPUT.TXT");
            inputFilePath = Path.GetFullPath(inputFilePath);
            outputFilePath = Path.GetFullPath(outputFilePath);
            Run(inputFilePath, outputFilePath);
        }

    }
}

