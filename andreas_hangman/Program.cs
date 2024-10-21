using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace andreas_hangman
{
    internal class Program
    {
        /// <summary>
        /// Hangman game class containing all the game logic.
        /// </summary>
        public class Hangman
        {
            string? word = null; // The word that the player will try to guess.
            private readonly StringBuilder hiddenWord = new(); // An abstraction of "word" so that it is hidden from the player.
            private readonly List<char> usedLettersBank = []; // Keeps track of already entered letters.
            int allowedFailiures = 6; // The amount of failed guesses allowed.

            /// <summary>
            /// Starts the Hangman game.
            /// </summary>
            public void BeginHangman()
            {
                if (word == null)
                {
                    Console.Write("Please insert a word to guess> ");
                    SetWord(Console.ReadLine());
                }

                // Pre-game setup.
                SetHiddenWord();

                // The start of the actual game.
                char enteredLetter;
                bool play = true;
                while (play)
                {
                    PrintHangman();
                    PrintHiddenWord();

                    enteredLetter = GetEnteredLetter();
                    if (LetterIsUsed(enteredLetter))
                    {
                        Console.WriteLine("Big yikes, u already used dat letter Xir!");
                        continue;
                    }

                    AddToLetterBank(enteredLetter);
                    if (!CheckLetter(enteredLetter))
                    {
                        Console.WriteLine("You lost a point!");
                        allowedFailiures--;
                    }

                    if (Defeat())
                    {
                        Console.WriteLine("You lost!");
                        Console.WriteLine("The word was " + word);
                        break;
                    }

                    if (Victory())
                    {
                        PrintHiddenWord();
                        Console.WriteLine("Congratiolations, you saved him!");
                        break;
                    }
                }
            }

            /// <summary>
            /// Checks if the player has been defeated.
            /// </summary>
            /// <returns>True if defeats, otherwise false.</returns>
            private bool Defeat()
            {
                if (allowedFailiures < 1)
                    return true;
                else
                    return false;
            }

            /// <summary>
            /// Checks if the player has won.
            /// </summary>
            /// <returns>True if victory, otherwise false.</returns>
            private bool Victory()
            {
                string hiddenWordString = hiddenWord.ToString();
                if (hiddenWordString == word)
                    return true;
                else
                    return false;
            }

            /// <summary>
            /// Checks if the entered letter is in the word.
            /// </summary>
            /// <param name="letter">The entered letter.</param>
            /// <returns>True if the letter is in the word, otherwise false.</returns>
            private bool CheckLetter(char letter)
            {
                bool checker = false;
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == letter)
                    {
                        checker = true;
                        hiddenWord[i] = letter;
                    }
                }

                return checker;
            }

            /// <summary>
            /// Checks if the letter has already been used.
            /// </summary>
            /// <param name="letter">The entered letter.</param>
            /// <returns>True if the letter is used, otherwise false.</returns>
            private bool LetterIsUsed(char letter)
            {
                if (usedLettersBank.Contains(letter))
                    return true;
                else
                    return false;
            }

            /// <summary>
            /// Adds a letter to the list of used letters.
            /// </summary>
            /// <param name="letter">The entered letter.</param>
            private void AddToLetterBank(char letter) { usedLettersBank.Add(letter); }

            /// <summary>
            /// Prints the word to console.
            /// </summary>
            public void PrintWord() { Console.WriteLine(word); }

            /// <summary>
            /// Sets the word to guess.
            /// </summary>
            /// <param name="passedWord">The word to be set.</param>
            public void SetWord(string passedWord)
            {
                passedWord = passedWord.ToLower();
                word = passedWord;
            }

            /// <summary>
            /// Prints the current state of the hidden word to console.
            /// </summary>
            private void PrintHiddenWord()
            {
                Console.WriteLine(hiddenWord);
            }

            /// <summary>
            /// Initializes the hidden word with underscores for each letter in the word, or when it finds a space, it appends a space to the hidden word.
            /// </summary>
            private void SetHiddenWord()
            {
                int wordLength = word.Length;
                hiddenWord.Clear();
                for (int i = 0; i < wordLength; i++)
                {
                    if (word[i] == ' ')
                        hiddenWord.Append(' ');
                    else
                        hiddenWord.Append('_');
                }
            }

            /// <summary>
            /// Gets the entered letter from console.
            /// </summary>
            /// <returns>The entered letter.</returns>
            private char GetEnteredLetter()
            {
                Console.Write("Guess letter> ");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.WriteLine();

                char input = keyInfo.KeyChar;

                return input;
            }

            /// <summary>
            /// Prints the current number of allowed failures to console.
            /// </summary>
            private void PrintHangman() { Console.WriteLine(allowedFailiures); }
        }

        static void Main(string[] args)
        {
            Hangman hangman = new Hangman();
            hangman.SetWord("I am Ironman");
            hangman.BeginHangman();
        }
    }
}
