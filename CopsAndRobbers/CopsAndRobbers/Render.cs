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
            Console.SetCursorPosition(0, position);
            for (int i = 0; i < (news.Count() < 5 ? news.Count() : 5); i++)
            {
                Console.WriteLine(news.ElementAt(news.Count() - i - 1));
            }
        }
    }
}
