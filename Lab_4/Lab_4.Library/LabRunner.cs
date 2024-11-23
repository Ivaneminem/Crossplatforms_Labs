using System;
using Lab_2;
using Lab_3;
using Lab_1;
using System.Text;

namespace Lab_4.Library
{
    public class LabRunner
    {
        public void RunLab_1(string input, string output)
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;


                Lab_1.Program.Run(input, output);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Lab_1: {ex.Message}");
            }
        }

        public void RunLab_2(string input, string output)
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;


                Lab_2.Program.Run(input, output);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Lab_2: {ex.Message}");
            }
        }
        public void RunLab_3(string input, string output)
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;


                Lab_3.Program.Run(input, output);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Lab_3: {ex.Message}");
            }
        }
    }
}
