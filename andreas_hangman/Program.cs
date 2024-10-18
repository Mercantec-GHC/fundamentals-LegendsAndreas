using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace andreas_hangman
{
    internal class Program
    {
        public class Hangman
        {
            string word = null;
            StringBuilder hiddenWord = new StringBuilder();
            //List<char> letterBank = new List<char>() {'a','b','c','e','f','g','h','i','j','k','l','m','n','o','p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'æ', 'ø', 'å' };
            List<char> usedLettersBank = new List<char>() { };
            int allowedFailiures = 6;

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
                    CheckLetter(enteredLetter);
                    // IsLetterValid
                }
            }

            public void CheckLetter(char letter)
            {
                List<int> indexes = new List<int>();
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == letter)
                    {
                    }
                }
            }

            private bool LetterIsUsed(char letter)
            {
                if (usedLettersBank.Contains(letter))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            private void AddToLetterBank(char letter)
            {
                usedLettersBank.Add(letter);
            }

            public void PrintWord()
            {
                Console.WriteLine(word);
            }

            public void SetWord(string passedWord)
            {
                passedWord = passedWord.ToLower();
                word = passedWord;
            }

            private void PrintHiddenWord()
            {
                Console.WriteLine(hiddenWord);
            }

            private void SetHiddenWord()
            {
                int wordLength = word.Length;
                string underScores = "";
                for (int i = 0; i < wordLength; i++)
                {
                    underScores += "_";
                }

                hiddenWord.Clear();
                hiddenWord.Append(underScores);
            }

            private char GetEnteredLetter()
            {
                Console.Write("Guess letter> ");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.WriteLine();

                char input = keyInfo.KeyChar;

                return input;
            }



            public void PrintHangman()
            {
                Console.WriteLine(allowedFailiures);
            }
        }

        static void Main(string[] args)
        {
            Hangman hangman = new Hangman();
            hangman.SetWord("Apple");
            hangman.BeginHangman();
        }
    }
}
