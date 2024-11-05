using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopsAndRobbers
{
    internal class People
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int DirX { get; set; }
        public int DirY { get; set; }
        public virtual List<Goods> Inventory { get; set; }
        public int MaxX { get; set; }
        public int MaxY { get; set; }


        public People(string name, int id, Location location)
        {
            Name = name;
            Id = id;
            SetPosition(location);
            SetDirection();
        }
        public void SetPosition(Location location)
        {
            Random random = new Random();
            PosX = random.Next(1,location.Width);
            PosY = random.Next(1,location.Height);
            MaxX = location.Width;
            MaxY = location.Height;
        }
        public void SetDirection()
        {
            var directions = new List<(int, int)>
            {
            { (0, 1) },    // Up
            { (0, -1) },   // Down
            { (1, 0) },    // Right
            { (-1, 0) },   // Left
            { (1, 1) },    // Up-right
            { (-1, 1) },   // Up-left
            { (1, -1) },   // Down-right
            { (-1, -1) }   // Down-left
            };
            var newDirections = new List<(int, int)>();

            foreach (var direction in directions)
            {
                int newX = PosX + direction.Item1;
                int newY = PosY + direction.Item2;

                // Check if the target position is within grid bounds
                if ((PosX + DirX != 0 || PosX + DirX != MaxX ||
                      PosY + DirY != 0 || PosY + DirY != MaxY))
                {
                    newDirections.Add(direction);
                }
            }

            Random rand = new Random();
            int rndDir = rand.Next(0, newDirections.Count());

            DirX = newDirections.ElementAt(rndDir).Item1;
            DirY = newDirections.ElementAt(rndDir).Item2;
        }
        public void Move(Location location)
        {
            
            if (PosX + DirX == 0 || PosX + DirX == MaxX ||
                PosY + DirY == 0 || PosY + DirY == MaxY )
            {
                location.CityGrid[(this.PosX, this.PosY)].Remove(this.Id);
                SetDirection();
            }
            else
            {
                Console.SetCursorPosition(PosX, PosY);
                Console.Write(" ");
                location.CityGrid[(this.PosX, this.PosY)].Remove(this.Id);
                PosY += DirY;
                PosX += DirX;

            }
            
        }
        public virtual void Interaction(People people, Location location)
        {
            location.News.Add($"{this.Name} hälsar på {people.Name}.                ");
        }
    } 

    

    class Citizen : People
    {
        private List<Goods> Belongings;
        public override List<Goods> Inventory { get { return this.Belongings; } set => this.Belongings = value; }
        public Citizen(string name, int id, Location location) : base(name, id, location)   
        {
            Belongings = new List<Goods>();
            CreateGoods(Belongings);
        }

        private void CreateGoods(List<Goods> goods)
        {
            goods.Add(new ("keys"));
            goods.Add(new ("phone"));
            goods.Add(new ("dosh"));
            goods.Add(new ("watch"));
        }
        public override void Interaction(People people, Location location)
        {
            if (people is Robber)
            {
                people.Interaction(this, location);
                
            }
            else
            {
                base.Interaction(people, location);
            }
        }
    }
    class Robber : People
    {
        private List<Goods> Loot;
        public override List<Goods> Inventory { get => this.Loot; set => this.Loot = value; }
        public Robber(string name, int id, Location location) : base(name, id, location)
        {
            Loot = new List<Goods>();
        }
        public override void Interaction(People people, Location location)
        {
            if (people is Citizen && people.Inventory.Count() > 0)
            {
                StealFrom(people);
                location.News.Add($"{this.Name} stal {this.Inventory.Last().ItemName} från {people.Name}.                ");
            }
            else if (people is Cop)
            {
                people.Interaction(this, location);
            }
            else
            {
                base.Interaction(people, location);
            }
        }
        private void StealFrom(People people)
        {
            Random rnd = new Random();
            int atIndex = rnd.Next(0, people.Inventory.Count());
            Inventory.Add(people.Inventory[atIndex]);
            people.Inventory.RemoveAt(atIndex);
        }
    }

    class Cop : People
    {
        private List<Goods> SeizedGoods;
        public override List<Goods> Inventory { get => this.SeizedGoods; set => this.SeizedGoods = value; }
        public Cop(string name, int id, Location location) : base(name, id, location)
        {
            SeizedGoods = new List<Goods>();
        }
        public override void Interaction(People people, Location location)
        {
            if (people is Robber && people.Inventory.Count() > 0)
            {
                this.SeizedFrom(people);
                location.News.Add($"{this.Name} beslagtog {this.Inventory.Count()} stöldgods från {people.Name}.                  ");
            }
            else
            {
                base.Interaction(people, location);
            }
        }
        private void SeizedFrom(People people)
        {
            for (int i = 0; i < people.Inventory.Count(); i++)
            {
                Inventory.Add(people.Inventory[i]);
            }
            people.Inventory.Clear();

        }

    }

}
