using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopsAndRobbers
{
    internal class Render
    {
        public static void NewsFeed(List<string> news, int position)
        {
            
            for (int i = 0; i < (news.Count() < 5 ? news.Count() : 5); i++)
            {
                Console.SetCursorPosition(position, i);
                Console.WriteLine($"{news.Count()-1-i}. {news.ElementAt(news.Count() - i - 1)}");
                
            }
            Thread.Sleep(1000);
        }
    }
}
