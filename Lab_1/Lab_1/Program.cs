using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab_1
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Формуємо повний шлях до файлу INPUT.TXT
            string inputFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "INPUT.TXT");
            string outputFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "OUTPUT.TXT");
            inputFilePath = Path.GetFullPath(inputFilePath);
            outputFilePath = Path.GetFullPath(outputFilePath);
            Run(inputFilePath, outputFilePath);
        }

        public static void Run(string inputFilePath, string outputFilePath)
        {
            // Читання вхідних даних з файлу
            var lines = File.ReadAllLines(inputFilePath);
            int n = int.Parse(lines[0]);

            // Створення списку для зберігання замовлень (остання дата та винагорода)
            List<(int Deadline, int Reward)> orders = new List<(int, int)>();
            for (int i = 1; i <= n; i++)
            {
                var parts = lines[i].Split();
                int t = int.Parse(parts[0]); // термін виконання замовлення
                int c = int.Parse(parts[1]); // винагорода за замовлення
                orders.Add((t, c));
            }

            int maxReward = CalculateMaxReward(orders);

            // Запис результату у файл OUTPUT.TXT
            File.WriteAllText(outputFilePath, maxReward.ToString());
        }

        public static int CalculateMaxReward(List<(int Deadline, int Reward)> orders)
        {
            foreach (var order in orders)
            {
                if (order.Deadline > 1000)
                {
                    throw new ArgumentException("Некоректний дедлайн замовлення. Максимально допустиме значення - 1000.");
                }
            }

            // Сортування замовлень за винагородою в порядку спадання
            orders = orders.OrderByDescending(order => order.Reward).ToList();

            // Масив для відстеження виконання замовлень по днях
            bool[] days = new bool[1001]; // Можна виконати до 1000 замовлень

            int maxReward = 0;

            foreach (var order in orders)
            {
                // Спроба виконати замовлення у найближчий можливий день (від кінця до початку)
                for (int day = order.Deadline; day > 0; day--)
                {
                    if (!days[day])
                    {
                        days[day] = true;
                        maxReward += order.Reward;
                        break;
                    }
                }
            }

            return maxReward;
        }
    }
}
