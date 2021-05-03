using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace hangman_v2
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the hangman game!");
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("\t1. Play");
            Console.WriteLine("\t2. Manage words");
            Console.Write("\nSelection: ");
            string choose = Console.ReadLine();

            switch (choose)
            {
                case "1":
                    Game game = new Game();
                    game.Play();
                    break;

                case "2":
                    string choice = "";
                    do
                    {
                        Console.Clear();

                        string path = AppDomain.CurrentDomain.BaseDirectory + "listWords.txt";

                        using (StreamReader sr = new StreamReader(path))
                        {
                            while (!sr.EndOfStream)
                            {
                                Console.WriteLine("\t- " + sr.ReadLine());
                            }
                        }

                        using (StreamWriter sw = File.AppendText(path))
                        {
                            Console.Write("\nAdd a word: ");

                            string word = Console.ReadLine();
                            sw.WriteLine(word);

                            sw.Close();

                            Console.Write("\nAdd another word? [y/n] - ");
                            choice = Console.ReadLine();
                        }

                    } while (choice == "y");

                    if (choice == "n")
                        Menu();
                    break;

                default:
                    Menu();
                    break;
            }

            if (choose == "1")
            {
                Console.Write("Do you want to play again ? [y/n] - ");
                string select = Console.ReadLine();
                if (select == "y")
                    Menu();
                else
                    Environment.Exit(0);
            }
        }
    }
}