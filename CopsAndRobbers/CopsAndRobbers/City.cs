using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopsAndRobbers
{
    internal class City
    {
        public int Size { get; }
        public List<string> News { get; set; }
        public List<Robber> Prisoners { get; set; }
        public List<People> Peoples { get; set; }
        public int AmmountOfCitizen { get; }
        public int AmmountOfTheifs { get; }
        public int AmmountOfCops { get; }
        public int[,] CityGrid { get; set; }

        public City(int size, int ammountOfCitizen, int ammountOfTheifs, int ammountOfCops)
        {
            Size = size;
            CityGrid = new int[size, size];
            Peoples = new List<People>();
            CreatePeople(Peoples, ammountOfCitizen, ammountOfTheifs, ammountOfCops);
            InitCityGrid();
        }

        public void InitCityGrid()
        {
            for (int i = 0; i < Peoples.Count; i++)
            {
                CityGrid[Peoples[i].PosX, Peoples[i].PosY] = i;
            }
        }

        public void DisplayCity()
        {
            char[,] city = new char[Size,Size];
            for (int row = 0; row < Size; row++)
            {
                if (row == 0 || row  == Size-1)
                {
                    
                }
                for (int col = 0; col < Size; col++)
                {
                    
                }
            }
        }
        public void CreatePeople(List<People> peoples, int ammountOfCitizen, int ammountOfTheifs, int ammountOfCops)
        {
            Random rnd = new Random();
            
            for (int i = 0; i < ammountOfCitizen; i++)
            {
                peoples.Add(new Citizen($"Medbergoare{i}", rnd.Next(1,Size), rnd.Next(1, Size), rnd.Next(-2,2), rnd.Next(-2, 2)));
            }
            for (int i = 0; i < ammountOfTheifs; i++)
            {
                peoples.Add(new Robber($"Tjuv{i}", rnd.Next(1, Size), rnd.Next(1, Size), rnd.Next(-2, 2), rnd.Next(-2, 2)));
            }
            for (int i = 0; i < ammountOfCops; i++)
            {
                peoples.Add(new Cop($"Polis{i}", rnd.Next(1, Size), rnd.Next(1, Size), rnd.Next(-2, 2), rnd.Next(-2, 2)));
            }
        }
    }
}
