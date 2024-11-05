using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopsAndRobbers
{
    internal class Location
    {
        public int StartPosX { get; set; }
        public int StartPosY { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public List<string> News { get; set; }
        public bool NewNews { get; set; }

        public List<People> Peoples { get; set; }
        
        public Dictionary<(int, int), List<int>> CityGrid { get; set; }

        public Location(int height, int width, int startPosX, int startPosY)
        {
            Height = height;
            Width = width;
            StartPosX = startPosX;
            StartPosY = startPosY;
            CityGrid = new Dictionary<(int, int), List<int>>();
            Peoples = new List<People>();
        }
       
    }
    class City: Location
    {
        public int AmmountOfCitizen { get; }
        public int AmmountOfTheifs { get; }
        public int AmmountOfCops { get; }
        
        public City(int ammountOfCitizen, int ammountOfTheifs, int ammountOfCops, int height, int width, int startPosX, int startPosY) : base(height, width, startPosX, startPosY)
        {
            News = new List<string>();
            CreatePeople(Peoples, ammountOfCitizen, ammountOfTheifs, ammountOfCops);
            // InitCityGrid();

        }
        public void InitCityGrid()
        {
            for (int i = 0; i < Peoples.Count(); i++)
            {
                if (CityGrid.TryGetValue((Peoples[i].PosX, Peoples[i].PosY), out List<int> indexList))  //Bryta ut till egen metod... Fixat
                {

                    indexList.Add(Peoples[i].Id);

                }
                else
                {
                    CityGrid.Add((Peoples[i].PosX, Peoples[i].PosY), new List<int> { Peoples.IndexOf(Peoples[i]) });
                }
                Render.DisplayPeople(Peoples[i]);
            }
        }

        public void UpdateCityGrid(People people)
        {
            
            if (CityGrid.TryGetValue((people.PosX, people.PosY), out List<int> indexList))  //Bryta ut till egen metod... Fixat
            {
                for (int i = 0; i < indexList.Count(); i++)
                {
                    people.Interaction(Peoples[indexList[i]], this); // Skapa interaction
                    NewNews = true;
                }
                indexList.Add(people.Id);
                
                //CityGrid.Add(people.PosX, people.PosY), 
               
            }
            else
            {
                CityGrid.Add((people.PosX, people.PosY), new List<int> { people.Id });
            }
        }

        private void CreatePeople(List<People> peoples, int ammountOfCitizen, int ammountOfTheifs, int ammountOfCops)
        {
            Random rnd = new Random();

            for (int i = 0; i < ammountOfCitizen; i++)
            {
                peoples.Add(new Citizen($"Medborgare{i}", peoples.Count(), rnd.Next(1, Width), rnd.Next(1, Height), rnd.Next(-1, 2), rnd.Next(-1, 2)));
            }
            for (int i = 0; i < ammountOfTheifs; i++)
            {
                peoples.Add(new Robber($"Tjuv{i}", peoples.Count(), rnd.Next(1, Width), rnd.Next(1, Height), rnd.Next(-1, 2), rnd.Next(-1, 2)));
            }
            for (int i = 0; i < ammountOfCops; i++)
            {
                peoples.Add(new Cop($"Polis{i}", peoples.Count(), rnd.Next(1, Width), rnd.Next(1, Height), rnd.Next(-1, 2), rnd.Next(-1, 2)));
            }
        }

    }
    class Prison: Location
    {
        public Prison(int height, int width, int startPosX, int startPosY) : base (width, height, startPosX, startPosY)
        {
            
        }
    }
}
