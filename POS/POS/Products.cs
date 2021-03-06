﻿using System;
using System.Collections.Generic;
using System.IO;

namespace POS
{
    static class Products
    {
        // string filoPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "OmahaGames"); just another way to set directory
        // string clientDetails = clientNameTextBox.Text + "," + mIDTextBox.Text + "," + billToTextBox.Text;

        public static void CheckCsvFile(string path)
        {
            FileInfo fileInfo = new FileInfo(path);

            if (!fileInfo.Exists)
                Directory.CreateDirectory(fileInfo.Directory.FullName);
            if (!File.Exists(path))
            {
                string clientHeader = "Product Name" + "," + "Category" + "," + "Description" + "," + "Price" + Environment.NewLine;
                File.WriteAllText(path, clientHeader);

                List<string> productList = new List<string>();
                foreach (var item in GenerateDefaultList())
                {
                    if (item == null)
                        continue;

                    string entry = item.Name + ',' + item.Category + ',' + item.Description + ',' + item.Price.ToString();

                    productList.Add(entry);
                }                  
                
                File.AppendAllLines(path, productList);
            }
        }

        public static List<Game> Games = new List<Game>();            
        public static void ListProducts()
        {
            List<Game> games = new List<Game>();

            string filePath = @"C:\Users\Public\Documents\OmahaGames\productlist.csv";
            CheckCsvFile(filePath);

            var linesInFile = File.ReadAllLines(filePath);
            string[] withFirstBitMissing = new string[linesInFile.Length - 1];
            Array.Copy(linesInFile, 1, withFirstBitMissing, 0, withFirstBitMissing.Length);
            foreach (var line in withFirstBitMissing)
            {
                string name = line.Split(',')[0];
                string category = line.Split(',')[1];
                string description = line.Split(',')[2];
                double price = Convert.ToDouble(line.Split(',')[3]);

                Game thisGame = new Game(name, category, description, price);

                Games.Add(thisGame);
            }
        }

        public static List<Game> GenerateDefaultList()
        {
            var a = new List<Game>();

            // Action (Mario Odyssey, Mega Man 11, Call of Duty: Black Ops 4)
            a.Add(new Game("Super Mario Odyssey", "Action", "Mario and Cappy a spirit that possesses Mario's hat and allows him to take control of other characters and objects set on a journey across various worlds to save Princess Peach from his nemesis Bowser.", 59.99));
            a.Add(new Game("Mega Man 11", "Action", "Robot boy goes pew-pew.", 39.99));
            a.Add(new Game("Call of Duty: Black Ops 4", "Action", "Army bois.", 59.99));

            // Sports (FIFA 19, Madden NFL 19, NHL 19)
            a.Add(new Game("FIFA 19", "Sports", "FIFA 18 is a 2017 football simulation video game in the FIFA series.", 49.99));
            a.Add(new Game("Madden NFL 19", "Sports", "North American football brought to you by the legend John Madden.", 49.99));
            a.Add(new Game("NHL 19", "Sports", "Ice hockey is cool.", 44.99));

            // Adventure (RDR, Monster Hunter, Farcry 5)
            a.Add(new Game("Red Dead Redemption 2", "Adventure", "GTA with cowboys.", 59.99));
            a.Add(new Game("Monster Hunter: World", "Adventure", "Take down brutal creatures in a vast fantasy landscape.", 59.99));
            a.Add(new Game("Farcry 5", "Adventure", "Wild animals and bad guys with guns stand in your way as you thwart some evil plot in the jungle.", 54.99));

            // Fighting (Dragon Ball FighterZ, Soul Calibur VI, Super Smash Bros Ultimate)
            a.Add(new Game("Soul Calibur VI", "Fighting", "Soulcalibur VI serves as a reboot to the series taking place during the 16th century to revisit the events of the first Soulcalibur game to \"uncover hidden truths\"", 59.99));
            a.Add(new Game("Dragon Ball FighterZ", "Fighting", "Take on all the best warriors this side of Nameka and beyond.", 44.99));
            a.Add(new Game("Super Smash Bros Ultimate", "Fighting", "All of your favorite Nintendo characters from across several franchises come together in this brawler (except Waluigi).", 59.99));

            return a;
        }
    }
}
