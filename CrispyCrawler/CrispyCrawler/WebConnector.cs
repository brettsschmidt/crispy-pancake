using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CrispyCrawler
{
    /// <summary>
    /// Class containing static functions for connecting to the web.
    /// </summary>
    class WebConnector
    {
        public static async Task<string> GetHTML(string link)
        {

            using (var client = new HttpClient())
            {
                var html = client.GetStringAsync(link);
                return html.Result;
            }

        }
    }
}

