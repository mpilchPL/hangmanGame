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

        private char[] coverPassword(char[] pass)
        {
            char[] coveredPassword = pass;
            for (int i = 0; i < pass.Length; i++)
            {
                if (!coveredPassword[i].Equals(' '))
                    coveredPassword[i] = '_';
            }
            return coveredPassword;
        }

        private void checkIfWon()
        {
            string p = new string(password);
            if (!(p.Contains('_')))
            {
                password = capital.ToCharArray();
                gameOver(true);
            }
        }

        private void checkIfLost()
        {
            if (lives < 1)       // player dead
            {
                gameOver(false);    // bool param stands for won?
            }
        }

        private void getNextGuess()
        {
            string line = Console.ReadLine().ToUpper();
            switch (line)
            {
                case "exit":    // EXIT GAME
                    exitGame();
                    break;

                default:
                    playersTries++;
                    if (line.Length > 1)         // USER TYPED WORD
                    {
                        if (line.Equals(capital))
                        {
                            gameOver(true);
                        }
                        else { this.lives -= 2; } // wrong word -2 life pts
                    }
                    else if (line.Length == 1)   // USER TYPED CHAR
                    {
                        if (wrongLetters.Contains(line))
                            return;
                        wrongLetters.Add(line);
                        if (capital.Contains(line)) // we have a match
                        {
                            unveilLetters(line, getCharsIndexes(line));
                        }
                        else   // miss -1 life pt
                        {
                            this.lives -= 1;
                        }
                    }
                    break;
            }
        }

        private void unveilLetters(string letter, List<int> indexes)
        {
            foreach (int i in indexes)
            {
                password[i] = letter[0];
            }
        }

        private void gameOver(bool win)
        {
            this.gameOn = false;
            timer.Stop();
            TimeSpan ts = timer.Elapsed;

            if (win) // game ending, player won
            {
                Console.Clear();
                Console.WriteLine(password);
                Console.WriteLine("Congratulations you won!. " +
                    "You managed to reveal the password in {0} seconds. " +
                    "Also it took you {1} tries to do it.", ts.Seconds, playersTries);
                Console.WriteLine("Type your name for highscores:");
                string name = Console.ReadLine();
                Program.putRecordToHighscores(name, ts.Seconds, playersTries, capital);
                exitGame();
            }
            else   // game ending, player lost
            {
                Console.WriteLine("You lost, type any key to exit.");
                Console.ReadLine();
                exitGame();
            }
        }



        private void exitGame()
        {
            this.gameOn = false;
            Console.Clear();
            Program.displayAvailableOptions();
        }

        private void setCountryCapital(string line)      // splits given line and sets variables country n capital
        {
            string separator = " | ";
            string[] separatedCountryCapital = line.Split(separator);

            this.country = separatedCountryCapital[0].ToUpper();
            this.capital = separatedCountryCapital[1].ToUpper();

            Console.WriteLine("Country: {0}, Capital: {1}", country, capital);
        }

        private List<int> getCharsIndexes(string c)      // this method finds indexes on which appears a given letter in a string
        {
            var letter = c.ToUpper();
            List<int> chars = new List<int>();

            for (int i = 0; i < capital.Length; i++)
            {
                if (capital.ToUpper().Substring(i, 1).Equals(letter))
                    chars.Add(i);

            }
            return chars;
        }

        private void renderInfo()
        {
            string letters = "";
            if (!(wrongLetters.Count < 1))
            {
                foreach (string l in wrongLetters)
                {
                    letters += "[" + l + "] ";
                }
            }

            Console.WriteLine("You can exit game at any time by typing command 'exit'.");
            Console.WriteLine("Lives: " + lives);
            Console.WriteLine("You have already tried those letters: " + letters);

        }

        private void renderPassword()
        {
            Console.WriteLine(password);

        }


    }
}
