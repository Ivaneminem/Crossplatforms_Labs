using System;
using System.Collections.Generic;
using System.IO;

public class Program
{
    static void Main()
    {
        string inputFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "INPUT.TXT");
        string outputFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "OUTPUT.TXT");
        inputFilePath = Path.GetFullPath(inputFilePath);
        outputFilePath = Path.GetFullPath(outputFilePath);
        // Зчитування вхідних даних
        string[] input = File.ReadAllLines(inputFilePath);
        int n = int.Parse(input[0]); // Розмір матриці
        int[,] matrix = new int[n, n];
        for (int i = 0; i < n; i++)
        {
            string[] row = input[i + 1].Split();
            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = int.Parse(row[j]);
            }
        }

        int[,] result = ReplaceZerosWithClosestNonZero(matrix, n);

        // Запис результату у файл
        using (StreamWriter writer = new StreamWriter(outputFilePath))
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    writer.Write(result[i, j]);
                    if (j < n - 1) writer.Write(" ");
                }
                writer.WriteLine();
            }
        }
    }

    public static int[,] ReplaceZerosWithClosestNonZero(int[,] matrix, int n)
    {
        int[,] result = new int[n, n];
        bool[,] visited;

        // Initialize result matrix with input values
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (matrix[i, j] < 0)
                {
                    throw new ArgumentException("Matrix elements must be non-negative integers.");
                }
                result[i, j] = matrix[i, j];
            }
        }

        // Напрямки для BFS: вгору, вниз, вліво, вправо
        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };

        // BFS для кожного елемента з нульовим значенням
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (matrix[i, j] == 0)
                {
                    Queue<(int, int, int)> queue = new Queue<(int, int, int)>();
                    queue.Enqueue((i, j, 0));
                    visited = new bool[n, n];
                    visited[i, j] = true;

                    int closestValue = -1;
                    int minDistance = int.MaxValue;
                    bool multipleClosest = false;

                    while (queue.Count > 0)
                    {
                        var (x, y, distance) = queue.Dequeue();
                        
                    if (matrix[x, y] != 0)
                        {
                            if (distance < minDistance)
                            {
                                closestValue = matrix[x, y];
                                minDistance = distance;
                                multipleClosest = false;
                            }
                            else if (distance == minDistance && matrix[x, y] != closestValue)
                            {
                                multipleClosest = true;
                            }
                        }

                        // Додаємо сусідні клітинки до черги
                        for (int d = 0; d < 4; d++)
                        {
                            int nx = x + dx[d];
                            int ny = y + dy[d];

                            if (nx >= 0 && nx < n && ny >= 0 && ny < n && !visited[nx, ny])
                            {
                                queue.Enqueue((nx, ny, distance + 1));
                                visited[nx, ny] = true;
                            }
                        }
                    }

                    // Оновлюємо результат тільки якщо знайдено одне найближче значення
                    if (!multipleClosest && closestValue != -1)
                    {
                        result[i, j] = closestValue;
                    }
                }
            }
        }

        return result;
    }
}
