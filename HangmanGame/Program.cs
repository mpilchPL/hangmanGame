using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HangmanGame
{
    class Program
    {
        const string CapitalsListFileName = "countries_and_capitals.txt";
        static string capitalsListFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\", CapitalsListFileName);

        const string WelcomeText = "Hangman Game \nWhat do you want to do?";
        static string[] options = { "Start Game", "Highscore", "Exit" };

        static void Main(string[] args)
        {
            bool programRunning = true;
            List<string> countryCapitals = getCapitalsFromFile();

            displayAvailableOptions();
            do
            {
                switch (Console.ReadLine())
                {
                    case "1": // NEW GAME
                        Game newGame = new Game(countryCapitals);
                        break;
                    case "2": // HIGHSCORE
                        //display highscore
                        break;
                    case "3": // EXIT GAME
                        programRunning = false;
                        break;
                    default:
                        Console.WriteLine("Please input a number of the option You want to choose.");
                        break;
                }

            } while (programRunning);
        }


        private static List<string> getCapitalsFromFile()
        {
            List<string> lines = new List<string>();
            Console.WriteLine("trying to get file from: " + capitalsListFilePath);
            if (File.Exists(capitalsListFilePath))
            {   // file found

                Console.WriteLine("File found");
                lines = File.ReadAllLines(capitalsListFilePath).ToList();

                return lines;
            }
            else
            {   // file not found

                Console.WriteLine("File NOT found");
                return new List<string> { "fake country | fake capital" };
            }
        }

        public static void displayAvailableOptions()
        {
            Console.WriteLine(WelcomeText);
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine("{0} - {1}", i + 1, options[i]);
            }
        }
    }
}
