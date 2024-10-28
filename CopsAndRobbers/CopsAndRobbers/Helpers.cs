﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CopsAndRobbers
{
    internal class Helpers
    {
        public static void Movement2(int currentPerson, Location location)
        {
            Random rnd = new Random();
            if (location.Peoples[currentPerson].PosX + location.Peoples[currentPerson].DirX == 0 || location.Peoples[currentPerson].PosX + location.Peoples[currentPerson].DirX == location.Width  ||
                location.Peoples[currentPerson].PosY + location.Peoples[currentPerson].DirY == 0 || location.Peoples[currentPerson].PosY + location.Peoples[currentPerson].DirY == location.Height  ||
                (location.Peoples[currentPerson].DirX == 0 && location.Peoples[currentPerson].DirY == 0))
            {
                location.Peoples[currentPerson].DirX = rnd.Next(-1, 2);
                location.Peoples[currentPerson].DirY = rnd.Next(-1, 2);
            }
            else
            {
                Console.SetCursorPosition(location.Peoples[currentPerson].PosX, location.Peoples[currentPerson].PosY);
                Console.Write(" ");
                //location.CityGrid[location.Peoples[currentPerson].PosX, location.Peoples[currentPerson].PosY] = null;
                location.Peoples[currentPerson].PosY += location.Peoples[currentPerson].DirY;
                location.Peoples[currentPerson].PosX += location.Peoples[currentPerson].DirX;
                //location.CityGrid[location.Peoples[currentPerson].PosX, location.Peoples[currentPerson].PosY] = currentPerson;
            }
        }
    }
}
