using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Owin.Logging;
using NinjaSoft.Web.Models;
using ILogger = NLog.ILogger;

namespace NinjaSoft.Web.Utils.Crawlers
{
    public abstract class CrawlerBase
    {
        internal static ILogger _log;
        internal IPoliticianRepository _repository;
      //  internal IHostingEnvironment _hostingEnvironment;

        protected CrawlerBase()
        {
            //_hostingEnvironment = hostingEnvironment;
            //_log = app.ApplicationServices
            //    .GetRequiredService<ILoggerFactory>()
            //    .CreateLogger("WebCrawler");

            //_repository = app.ApplicationServices
            //    .GetRequiredService<IPoliticianRepository>();

         

        }

        internal  static HtmlNode GetRootDocumentNode(string html)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);
            var root = document.DocumentNode;
            return root;
        }

        internal static async Task<object> SendRequest(string baseUri,string route)
        {
            //_log.LogInformation($"SendRequest:{baseUri}/{route}");
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(baseUri);
                    var response = await client.GetAsync(route);
                    response.EnsureSuccessStatusCode(); // Throw in not success
                    var content = await response.Content.ReadAsStreamAsync();

                    //return JsonConvert.DeserializeObject(stringResponse);
                    return content;
                }
                catch (HttpRequestException e)
                {
                   // _log.LogError($"Request exception: {e.Message}");
                }
            }
            return null;
        }

    }
}
