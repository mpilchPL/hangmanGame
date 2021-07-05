using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HangmanGame
{
    class Game
    {
        List<string> countryCapitals;
        private string capital, country;
        private int lives = 5;
        private bool gameOn = true;
        private List<string> wrongLetters = new List<string>();
        private int playersTries = 0;
        private char[] password;
        Stopwatch timer = new Stopwatch();
        public Game(List<string> lines)
        {
            countryCapitals = lines;           //initialize list of capitals
            int randomIndex = new Random().Next(countryCapitals.Count);
            setCountryCapital(countryCapitals[randomIndex]);
            password = coverPassword(capital.ToCharArray());
            timer.Start();
            do
            {
                Console.Clear();
                renderInfo();
                renderPassword();

                getNextGuess();
                checkIfLost();
                checkIfWon();

            } while (gameOn);
        }

        private char[] coverPassword(char[] pass)       // switches all letters in password to "_"
        {
            char[] coveredPassword = pass;
            for (int i = 0; i < pass.Length; i++)
            {
                if (!coveredPassword[i].Equals(' '))
                    coveredPassword[i] = '_';
            }
            return coveredPassword;
        }

        private void setCountryCapital(string line)      // splits given line and sets variables country n capital
        {
            string separator = " | ";
            string[] separatedCountryCapital = line.Split(separator);

            this.country = separatedCountryCapital[0].ToUpper();
            this.capital = separatedCountryCapital[1].ToUpper();

            Console.WriteLine("Country: {0}, Capital: {1}", country, capital);
        }

        private void renderInfo()
        {

        }
        private void renderPassword()
        {

        }

        private void getNextGuess()
        {

        }
        private void checkIfLost()
        {

        }
        private void checkIfWon()
        {

        }
    }

    
}
