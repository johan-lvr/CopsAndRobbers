﻿using System;
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


        public People(string name, int id, int posX, int posY, int dirX, int dirY)
        {
            Name = name;
            Id = id;
            PosX = posX;
            PosY = posY; 
            DirX = dirX; 
            DirY = dirY; 
        }
        public void Move(Location location)
        {
            Random rnd = new Random();
            if (PosX + DirX == 0 || PosX + DirX == location.Width ||
                PosY + DirY == 0 || PosY + DirY == location.Height ||
                (DirX == 0 && DirY == 0))
            {
                location.CityGrid[(this.PosX, this.PosY)].Remove(this.Id);
                DirX = rnd.Next(-1, 2);
                DirY = rnd.Next(-1, 2);
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
        public Citizen(string name, int id, int posX, int posY, int dirX, int dirY) : base(name, id, posX, posY, dirX, dirY)   
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
        public Robber(string name,int id, int posX, int posY, int dirX, int dirY) : base(name, id, posX, posY, dirX, dirY)
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
        public Cop(string name, int id, int posX, int posY, int dirX, int dirY) : base(name, id, posX, posY, dirX, dirY)
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
