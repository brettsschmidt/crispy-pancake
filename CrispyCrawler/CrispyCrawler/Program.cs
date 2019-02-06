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
            string link = "https://en.wikipedia.org/wiki/2000_in_film";
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
