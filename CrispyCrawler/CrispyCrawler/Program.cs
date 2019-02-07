using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrispyCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            string link = "https://en.wikipedia.org/wiki/The_Wolf_of_Wall_Street_(2019_film)";
            WikipediaParser wiki = new WikipediaParser();
            List<string> links = new List<string>();
            links.Add(link);
            wiki.AddLinks(links);
            Task.Run(wiki.GetPlotDataAsync);
            wiki.GetPlotDataAsync().Wait();

            Console.ReadLine();
        }
    }
}
