using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrispyCrawler
{
    class WikipediaParser
    {
        List<string> plots;
        List<string> links;
        List<string> gatheredLinks;

        public WikipediaParser()
        {
            plots = new List<string>();
            links = new List<string>();
            gatheredLinks = new List<string>();
        }

        public void AddLinks(List<string> morelinks)
        {
            foreach(string s in morelinks)
            {
                links.Add(s);
            }
        }

        public async Task GetPlotDataAsync()
        {
            Task<string>[] tasks = new Task<string>[links.Count];
            Task<string>[] decodeTasks = new Task<string>[links.Count];
            for(int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = WebConnector.GetHTML(links[i]);
                
            }
            

            for(int i = 0; i < decodeTasks.Length; i++)
            {
                int loc = Task.WaitAny(tasks);
                decodeTasks[i] = DecodeHTML(tasks[loc].Result);
            }
            for (int i = 0; i < decodeTasks.Length; i++)
            {
                int loc = Task.WaitAny(decodeTasks);
                plots.Add(decodeTasks[loc].Result);
            }
        }
        private async Task<string> DecodeHTML(string html)
        {
            List<string> curlinks = new List<string>();
            if(html.Contains("Plot"))
            {
                string scrubbedhtml = html.Substring(html.IndexOf("id=\"Plot\">Plot</span>"));
                scrubbedhtml = scrubbedhtml.Substring(scrubbedhtml.IndexOf("</h2>") + 5);
                scrubbedhtml = scrubbedhtml.Substring(0, scrubbedhtml.IndexOf("<h2>"));
                scrubbedhtml = scrubbedhtml.Replace("<p>", "")
                    .Replace("</p>", "")
                    .Replace("<i>", "")
                    .Replace("</i>", "");
                while(scrubbedhtml.Contains("a href="))
                {
                    string s = scrubbedhtml.Substring(scrubbedhtml.IndexOf("title="), scrubbedhtml.IndexOf("</a>") - scrubbedhtml.IndexOf("title="));
                    s = s.Substring(s.IndexOf('>') + 1);
                    string link = scrubbedhtml.Substring(scrubbedhtml.IndexOf("a href"), scrubbedhtml.IndexOf("title=") - scrubbedhtml.IndexOf("a href="));
                    link = link.Substring(link.IndexOf("\"") + 1, link.LastIndexOf("\"") - link.IndexOf("\""));
                    curlinks.Add(link);
                    scrubbedhtml = scrubbedhtml.Remove(scrubbedhtml.IndexOf("<a href"), scrubbedhtml.IndexOf("</a>") - scrubbedhtml.IndexOf("<a href") + 3);
                    scrubbedhtml = scrubbedhtml.Insert(scrubbedhtml.IndexOf(">"),  s );
                    scrubbedhtml = scrubbedhtml.Remove(scrubbedhtml.IndexOf(">"), 1);
                    scrubbedhtml = scrubbedhtml.Replace("\n", "");
                    
                }
                return scrubbedhtml;
            }
            else
            {
                return "";
            }
        }
    }
}
