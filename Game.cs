using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    internal class Game
    {
        static string guessedLetters = "";
        static int lives = 3;
        static string word;
        static bool wordGuessed;
        /// <summary>
        /// This method starts the game, and loops until word is guessed or if you have 0 lives left
        /// </summary>
        public static void SetupGame()
        {
            word = MainWord();
            while (!WordVisible() && lives > 0 && !wordGuessed)
            {
                WordDisplay(word, guessedLetters);
                StartTurn(word);
                WordVisible();
            }
            Console.WriteLine($"The word was: {word}");
        }
        /// <summary>
        /// Asks for the main word
        /// </summary>
        /// <returns></returns>
        static string MainWord()
        {
            Console.WriteLine("Please choose a word");
            string theWord = Console.ReadLine();
            Console.Clear();

            return theWord;
        }
        /// <summary>
        /// True or False, if the word is fully visible to end the game
        /// </summary>
        /// <returns></returns>
        static bool WordVisible()
        {
            foreach (char ch in word)
            {
                if (!guessedLetters.Contains(ch)) return false;
            }
            return true;
        }
        /// <summary>
        /// Starts your turn to guess
        /// </summary>
        /// <param name="word"></param>
        static void StartTurn(string word)
        {
            Console.WriteLine($"Lives left: {lives}");
            Console.WriteLine("Guess a letter or press f1 to guess a word");
            ConsoleKeyInfo keyPress = Console.ReadKey(true);
            if (keyPress.Key == ConsoleKey.F1) {lives = WordGuess(word); }
            else
            {
                char guessedLetter = char.ToLower(keyPress.KeyChar); // Ensure the guessed letter is lowercase

                if (guessedLetters.Contains(guessedLetter))
                {
                    Console.WriteLine("You've already guessed that letter.");
                }
                else
                {
                    guessedLetters += guessedLetter; // Add the letter to guessed letters
                    lives = LetterCheck(word, guessedLetter); // Update lives based on the guess
                }
            }

        }
        /// <summary>
        /// Makes the letters visible if you guessed the correct letters
        /// </summary>
        /// <param name="word"></param>
        /// <param name="guess"></param>
        static void WordDisplay(string word, string guess)
        {
            foreach (char ch in word.ToLower())
            {
                if (guess.ToLower().Contains(ch)) { Console.Write(ch+" "); }
                else Console.Write("_ ");
            }
        }
        /// <summary>
        /// Checks if the letter is inside the main word
        /// </summary>
        /// <param name="word"></param>
        /// <param name="keyChar"></param>
        /// <returns></returns>
        private static int LetterCheck(string word, char keyChar)
        {
            Console.Clear();
            if (word.Contains(keyChar)) { Console.WriteLine($"The word contains {keyChar}"); return lives; }
            else //(!word.Contains(keyChar))
            {
                Console.WriteLine("Incorrect guess"); return (lives -1);
            }
        }
        /// <summary>
        /// Makes it able to guess the full word if known
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private static int WordGuess(string word)
        {
            Console.Write("guess the word: ");
            string wordguess = Console.ReadLine();
            if (wordguess == word.ToLower()) { wordGuessed = true; return lives; }
            else { Console.WriteLine("Incorrect"); return (lives - 1); }
        }
    }
}