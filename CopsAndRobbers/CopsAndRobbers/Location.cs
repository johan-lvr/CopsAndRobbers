using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopsAndRobbers
{
    internal class Location
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public List<string> News { get; set; }
        public List<Robber> Prisoners { get; set; }
        public List<People> Peoples { get; set; }
        
        public int[,] CityGrid { get; set; }

        public Location(int height, int width)
        {
            Height = height;
            Width = width;
            CityGrid = new int[height, width];
            Peoples = new List<People>();
            //DisplayLocation();
        }
        public void DisplayPeople()
        {
            for (int i = 0; i < Peoples.Count; i++)
            {
                Console.SetCursorPosition(Peoples[i].PosX, Peoples[i].PosY);
                if (Peoples[i] is Citizen)
                {
                    Console.Write("C");
                }
                else if (Peoples[i] is Robber)
                {
                    Console.Write("R");
                }
                else
                {
                    Console.Write("P");
                }
            }
        }

        

        public void DisplayLocation()
        {
            
            for (int col = 0; col <= Height; col++)
            {
                for (int row = 0; row <= Width; row++)
                {

                    if (col == 0 || col == Height || row == 0 || row == Width)
                    {
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write(" ");
                    }


                }
                Console.WriteLine();
            }
        }
       
    }
    class City: Location
    {
        public int AmmountOfCitizen { get; }
        public int AmmountOfTheifs { get; }
        public int AmmountOfCops { get; }
        public City(int ammountOfCitizen, int ammountOfTheifs, int ammountOfCops, int height, int width) : base(height, width)
        {
            CreatePeople(Peoples, ammountOfCitizen, ammountOfTheifs, ammountOfCops);
            // InitCityGrid();
        }
        private void InitCityGrid()
        {
            for (int i = 0; i < Peoples.Count; i++)
            {
                CityGrid[Peoples[i].PosX, Peoples[i].PosY] = i;
            }
        }
        private void CreatePeople(List<People> peoples, int ammountOfCitizen, int ammountOfTheifs, int ammountOfCops)
        {
            Random rnd = new Random();

            for (int i = 0; i < ammountOfCitizen; i++)
            {
                peoples.Add(new Citizen($"Medbergoare{i}", rnd.Next(1, Width), rnd.Next(1, Height), rnd.Next(-2, 2), rnd.Next(-2, 2)));
            }
            for (int i = 0; i < ammountOfTheifs; i++)
            {
                peoples.Add(new Robber($"Tjuv{i}", rnd.Next(1, Width), rnd.Next(1, Height), rnd.Next(-2, 2), rnd.Next(-2, 2)));
            }
            for (int i = 0; i < ammountOfCops; i++)
            {
                peoples.Add(new Cop($"Polis{i}", rnd.Next(1, Width), rnd.Next(1, Height), rnd.Next(-2, 2), rnd.Next(-2, 2)));
            }
        }

    }
    class Prison: Location
    {
        public Prison(int height, int width) : base (width, height)
        {
            
        }
    }
}
