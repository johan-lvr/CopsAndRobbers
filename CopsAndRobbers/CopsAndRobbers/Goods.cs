using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopsAndRobbers
{
    internal class Goods
    {
        public string ItemName { get; set; }
        //public string OriginalOwner { get; }
        public Goods(string itemName)
        {
            ItemName = itemName;
        }
    }
}
