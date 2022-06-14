using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AppKonsolowa1
{
    class Program
    {
        static void Main(string[] args)
        {
            bool quitApp = false;
            bool mainMenuActive = true;
            bool gameMenuActive = false;
            bool stationMenuActive = false;
            int inputNumber;
            string input;
            bool isNumber;
            int numberGames;
            int numberStations = 0;
            IDictionary dictGame = new Dictionary<int, string>();//games list
            IDictionary dictStation = new Dictionary<int, string>();//station list of choosen game
            

            while (!quitApp)
            {
                if (mainMenuActive)
                {
                    MainMenuGreet();
                    dictGame = GamesList();
                    Instructions(mainMenuActive);
                    numberGames = dictGame.Count;
                    input = Console.ReadLine();
                    isNumber = int.TryParse(input, out inputNumber);
                    if (!isNumber)
                    {
                        continue;
                    }
                    if (inputNumber > 0 && inputNumber <= numberGames)
                    {
                        dictStation = StationList(dictGame[inputNumber].ToString());
                        mainMenuActive = false;
                        Instructions(mainMenuActive);
                        numberStations = dictStation.Count;
                        
                        gameMenuActive = true;
                    }
                    if (inputNumber == 00)
                    {
                        quitApp = true;
                    }
                }
                if (gameMenuActive)
                {
                    input = Console.ReadLine();
                    isNumber = int.TryParse(input, out inputNumber);

                    if (inputNumber > 0 && inputNumber <= numberStations)
                    {
                        TrackList(dictStation[inputNumber].ToString());
                        Instructions(mainMenuActive);
                        gameMenuActive = false;
                        stationMenuActive = true;
                    }
                    if (!isNumber)
                    {
                        continue;
                    }
                    //back
                    /*
                    if (inputNumber == 99)
                    {
                        gameMenuActive = false;
                        mainMenuActive = true;
                    }
                    */
                    if (inputNumber == 99)
                    {
                        mainMenuActive = true;
                        gameMenuActive = false;
                        stationMenuActive = false;
                    }

                    if (inputNumber == 00)
                    {
                        quitApp = true;
                    }
                }
                if (stationMenuActive)
                {
                    input = Console.ReadLine();
                    isNumber = int.TryParse(input, out inputNumber);
                    if (!isNumber)
                    {
                        continue;
                    }
                    //back
                    /*
                    if (inputNumber == 99)
                    {
                        stationMenuActive = false;
                        gameMenuActive = true;
                    }
                    */
                    if (inputNumber == 99)
                    {
                        mainMenuActive = true;
                        gameMenuActive = false;
                        stationMenuActive = false;
                    }

                    if (inputNumber == 00)
                    {
                        quitApp = true;
                    }
                }
            }
        }


        static IDictionary GamesList()
        {
            int gameNumber = 1;
            var gamesDict = new Dictionary<int, string>();
            string[] gamesList = Directory.GetDirectories(@"C:\csharp\Radio_Stations_of_GTA_Series\AppKonsolowa1\AppKonsolowa1\games", "*", System.IO.SearchOption.TopDirectoryOnly);
            Console.WriteLine("Lista Gier z serii Grand Theft Auto:\n");
            foreach (string game in gamesList)
            {
                string gameName = new System.IO.DirectoryInfo(game).Name;
                Console.WriteLine(gameNumber + " - " + gameName);
                gamesDict.Add(gameNumber, game);
                gameNumber += 1;
            }
            return gamesDict;

        }

        static IDictionary StationList(string gamePath)
        {
            string FolderName = new System.IO.DirectoryInfo(gamePath).Name;
            Console.Clear();
            Console.Write("Lista stacji ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Grand Theft Auto " + FolderName);
            Console.ResetColor();
            int stationNumber = 1;
            var stationsDict = new Dictionary<int, string>();
            string[] stationsList = Directory.EnumerateFiles(gamePath, "*.*").ToArray();
            foreach (string station in stationsList)
            {
                string stationName = Path.GetFileName(station);
                stationName = stationName.Substring(0, stationName.Length - 4);
                stationName = stationName.Substring(2, stationName.Length - 2);
                Console.WriteLine(stationNumber + " - " + stationName);
                stationsDict.Add(stationNumber, station);
                stationNumber += 1;
            }
            return stationsDict;
        }
        static void TrackList(string stationPath)
        {
            string fileName = Path.GetFileName(stationPath);
            fileName = fileName.Substring(0, fileName.Length - 4);
            fileName = fileName.Substring(2, fileName.Length - 2);
            Console.Clear();
            Console.Write("Lista utworów stacji ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(fileName);
            Console.ResetColor();
            int trackNumber = 1;
            string[] trackList = File.ReadAllLines(stationPath);
            foreach (string track in trackList)
            {
                Console.WriteLine(trackNumber + " - " + track);
                trackNumber += 1;
            }
        }

        static void MainMenuGreet()
        {
            Console.Clear();
            Console.Write("Witaj w aplikacji ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Radio Stations of Grand Theft Auto Series");
            Console.ResetColor();
        }

        static void Instructions(bool mainMenuActive)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            /*
            if (!mainMenuActive)
            {
                Console.WriteLine("99 - Cofnij");
            }
            */
            if (!mainMenuActive)
            {
                Console.WriteLine("99 - Menu start");
            }
            Console.WriteLine("00 - Zamknij aplikację");
            Console.ResetColor();
        }

    }
}
