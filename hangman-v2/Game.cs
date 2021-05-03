using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace hangman_v2
{
    class Game
    {
        private Word full_word;
        private string masked_word;
        private List<Word> list_words;
        private List<char> list_char_yes;
        private List<char> list_char_no;
        private bool victory;
        private int number_fail = 10;

        public Game()
        {
            list_words = new List<Word>();

            string path = AppDomain.CurrentDomain.BaseDirectory + "listWords.txt";
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    Word word = new Word(sr.ReadLine());
                    list_words.Add(word);
                }
                sr.Close();
            }

            Random random = new Random();
            full_word = list_words[random.Next(0, list_words.Count)];

            list_char_yes = new List<char>();
            list_char_no = new List<char>();

            masked_word = Mask(full_word);

            victory = false;
        }

        private string Mask(Word p_word)
        {
            char symbol = '_';
            string mask = "";
            string word = p_word.GetSetWord;

            for (int i = 0; i < p_word.GetLenght; i++)
            {
                if (list_char_yes.Contains(word[i]))
                    mask += word[i];
                else
                    mask += symbol + " ";
            }
            return mask;
        }

        public void Play()
        {
            Console.Clear();
            Console.WriteLine("The game starts!");

            int counter = 1;
            while (counter != number_fail + 1 && !victory)
            {
                Console.WriteLine("\nThe word is : " + masked_word);

                Console.WriteLine($"\nTries {counter}/{number_fail}.");
                Console.Write("Choose a letter : ");
                char letter = char.Parse(Console.ReadLine());

                if (full_word.PositionChar(letter) == -1)
                {
                    list_char_no.Add(letter);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nThe letter isn't in the word.");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.Write("List of denied letters : ");
                    for (int i = 0; i < list_char_no.Count; i++)
                    {
                        Console.Write(list_char_no[i] + " - ");
                    }
                    Console.Write("\n");

                    counter++;
                }
                else
                {
                    list_char_yes.Add(letter);
                    masked_word = Mask(full_word);

                    if (full_word.GetSetWord == masked_word)
                        victory = true;
                }

                Console.WriteLine("------------------------------------------------------");
            }

            if (victory)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"You won! You find the masked word with {counter} tries.");
                Console.WriteLine($"The word was: {full_word.GetSetWord}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"You lost! You didn't find the correct word with {number_fail} tries.");
                Console.WriteLine($"The word was: {full_word.GetSetWord}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}