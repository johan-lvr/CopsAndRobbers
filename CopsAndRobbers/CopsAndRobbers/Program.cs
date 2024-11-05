namespace CopsAndRobbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            
            // Instansierat subklass okbjekten city och prison
            City city = new City(12, 12, 12, 20, 80, 0, 0);
            Prison prison = new Prison(20, 10, 0, 22);

            //Ritar upp de visuella i konsollen från objekten
            Render.DisplayLocation(city);
            Render.DisplayLocation(prison);
            city.InitCityGrid();

            while (true)
            {
                //Går igenom alla element i city.Peoples lista
                foreach (People people in city.Peoples)
                {
                    //Kommer kommentar
                    people.Move(city);
                    Render.DisplayPeople(people);
                    city.UpdateCityGrid(people);
                    Render.NewsFeed(city, 34);
                }
                Thread.Sleep(100);
            }
        }
    }
}
