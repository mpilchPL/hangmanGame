using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HangmanGame
{
    class Program
    {
        const string CapitalsListFileName = "countries_and_capitals.txt";
        const string HighscoreFileName = "highscore.txt";

        const string WelcomeText = "Hangman Game \nWhat do you want to do?";
        static string[] options = { "Start Game", "Highscore", "Exit" };
        static string capitalsListFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\", CapitalsListFileName);
        static string highscoreFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\", HighscoreFileName);

        static void Main(string[] args)
        {
            bool programRunning = true;           // program loop condition
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
                        displayHighscore();
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
                if (File.Exists(capitalsListFilePath))
            {   // file found
                lines = File.ReadAllLines(capitalsListFilePath).ToList();
                return lines;
            }
            else
            {   // file not found

                Console.WriteLine("File {0} NOT found", CapitalsListFileName);
                return new List<string> { "fake country | fake capital" };
            }
        }

        public static void putRecordToHighscores(string name, int time, int tries, string city)
        {
            // save line to file ( create if theres no file)
            // line must includes: date, name, time, tries, city
            List<string> lines = new List<string>();
            if (File.Exists(highscoreFilePath))
                lines = File.ReadAllLines(highscoreFilePath).ToList();
            string date = DateTime.Today.ToString();
            string record = "Date: " + date + " | Player: " + name + " | Time: " + time + "s | Letters checked: " + tries + " | Capital: " + city;
            lines.Add(record);
            File.WriteAllLines(highscoreFilePath, lines);
        }

        private static void displayHighscore()
        {
            List<string> lines = new List<string>();
            if (File.Exists(highscoreFilePath))
            {
                lines = File.ReadAllLines(highscoreFilePath).ToList();
                foreach(string line in lines)
                {
                    Console.WriteLine(line);
                }
            }
            else
            {
                Console.WriteLine("No highscores yet. Try to win a game.");
                return;
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
