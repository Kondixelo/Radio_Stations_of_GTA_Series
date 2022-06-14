using System;

namespace AppKonsolowa1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Witaj w aplikacji ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Radio Stations of Grand Theft Auto Series");
            Console.ResetColor();
            string[] gamesList = GamesList();
            Console.ReadKey();

        }


        static string[] GamesList()
        {
            int gameNumber = 1;
            string[] gamesList = { "I", "2", "III", "Vice City", "San Andreas","Liberty City Stories", "Vice City Stories", "IV", "Chinatown Wars", "V" };
            Console.WriteLine("Lista Gier z serii Grand Theft Auto:\n");
            foreach (string game in gamesList)
            {
                Console.WriteLine(gameNumber + " - " + game);
                gameNumber += 1;
            }
            return gamesList;
        }
    }
}
