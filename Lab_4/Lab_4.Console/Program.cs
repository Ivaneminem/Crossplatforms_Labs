using System;
using System.IO;
using McMaster.Extensions.CommandLineUtils;
using Lab_4.Library;

namespace Lab_4
{
    [Command(Name = "LabRunnerApp", Description = "Utility for running lab programs")]
    [Subcommand(typeof(AppVersionAndAuthor), typeof(RunLab), typeof(ConfigurePath))]
    public class Program
    {
        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private void OnExecute()
        {
            Console.WriteLine("Specify a command");
        }

        private void OnUnknownCommand(CommandLineApplication app)
        {
            Console.WriteLine("Invalid command. Available commands:");
            Console.WriteLine(" - version: Show application version and author info");
            Console.WriteLine(" - run: Run a specific lab program");
            Console.WriteLine(" - set-path: Set up a custom input/output directory");
        }
    }

    [Command(Name = "version", Description = "Version and author info")]
    class AppVersionAndAuthor
    {
        private void OnExecute()
        {
            Console.WriteLine("Author: Bondarenko Ivan");
            Console.WriteLine("Version: 1.0.0");
        }
    }

    [Command(Name = "run", Description = "Run a lab program")]
    class RunLab
    {
        [Argument(0, "labName", "Specify the lab to execute (lab1, lab2, lab3)")]
        public string SelectedLab { get; set; } = string.Empty;

        [Option("-i|--input", "Specify input file", CommandOptionType.SingleValue)]
        public string InputPath { get; set; } = string.Empty;

        [Option("-o|--output", "Specify output file", CommandOptionType.SingleValue)]
        public string OutputPath { get; set; } = string.Empty;

        private void OnExecute()
        {
            var labRunner = new LabRunner();
            string labDirectory = LocateLabDirectory(SelectedLab);
            if (labDirectory == null)
            {
                Console.WriteLine($"Error: Lab '{SelectedLab}' is not recognized. Valid options: lab1, lab2, lab3.");
                return;
            }

            string inputFile = FindInputFile(InputPath);
            if (inputFile == null)
            {
                Console.WriteLine("Error: Input file not found.");
                return;
            }

            string outputFile = GenerateOutputFilePath(labDirectory);
            if (SelectedLab.ToLower() == "lab1")
            {
                labRunner.RunLab_1(inputFile, outputFile);
            }
            else if (SelectedLab.ToLower() == "lab2")
            {
                labRunner.RunLab_2(inputFile, outputFile);
            }
            else if (SelectedLab.ToLower() == "lab3")
            {
                labRunner.RunLab_3(inputFile, outputFile);
            }
            else
            {
                Console.WriteLine("Error: Unknown lab.");
            }

            Console.WriteLine($"Execution completed for {SelectedLab}. Results saved at {outputFile}");
        }

        private string FindInputFile(string filePath)
        {

            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                return filePath;
            }


            string environmentPath = Environment.GetEnvironmentVariable("LAB_PATH");
            if (!string.IsNullOrEmpty(environmentPath))
            {
                string envFilePath = Path.Combine(environmentPath, Path.GetFileName(filePath));
                if (File.Exists(envFilePath))
                {
                    return envFilePath;
                }
            }


            string userHome = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string homeFilePath = Path.Combine(userHome, Path.GetFileName(filePath));
            if (File.Exists(homeFilePath))
            {
                return homeFilePath;
            }


            return null;
        }

        private string GenerateOutputFilePath(string directory)
        {
            return Path.Combine(directory, "OUTPUT.txt");
        }

        private string LocateLabDirectory(string labName)
        {
            string baseDirectory = Directory.GetCurrentDirectory();

            return labName.ToLower() switch
            {
                "lab1" => Path.Combine(baseDirectory, "Lab_1"),
                "lab2" => Path.Combine(baseDirectory, "Lab_2"),
                "lab3" => Path.Combine(baseDirectory, "Lab_3"),
                _ => null
            };
        }
    }

    [Command(Name = "set-path", Description = "Set custom input/output directory")]
    class ConfigurePath
    {
        [Option("-p|--path", "Path to input/output files", CommandOptionType.SingleValue)]
        public string? DirectoryPath { get; set; }

        private void OnExecute()
        {
            if (string.IsNullOrEmpty(DirectoryPath))
            {
                Console.WriteLine("Error: Directory path cannot be empty.");
                return;
            }

            Environment.SetEnvironmentVariable("LAB_PATH", DirectoryPath);
            Console.WriteLine($"Directory set to: {DirectoryPath}");
        }
    }
}
