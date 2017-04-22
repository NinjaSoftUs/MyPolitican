using System;
using System.IO;
using System.Linq;
using System.Web;

namespace NinjaSoft.Web.Utils.Crawlers
{
    public class OpenSecretsCrawler : CrawlerBase
    {
        private string _baseDir;
        private const string BASE_URL = "https://www.opensecrets.org";
       
        //public OpenSecretsCrawler(IApplicationBuilder app, IHostingEnvironment hostingEnvironment) :base(app, hostingEnvironment)
        //{
        //}
        public OpenSecretsCrawler()
        {
            _baseDir = HttpContext.Current.Server.MapPath("~");
        }

        public void DownloadBioImgs()
        {
            var dirInfo = new DirectoryInfo($"{_baseDir}\\images\\Bio");

            var list = _repository.GetPoliticians().Select(x => x.Cid);
            foreach (var cid in list)
            {
                var fileInfo = new FileInfo($"{dirInfo.FullName}\\{cid}.jpg");
                if (!fileInfo.Exists)
                {


                    var baseUrl = "https://s3.amazonaws.com";
                    var route = $"assets2.opensecrets.org/politicians/img/{cid}.jpg";
                    // var response = SendRequest(BASE_URL,route).Result;
                    var response = SendRequest(baseUrl, route).Result;

                    try
                    {
                        // if the remote file was found, download oit
                        using (Stream inputStream = response as Stream)
                        using (Stream outputStream = File.OpenWrite($"{dirInfo.FullName}\\{cid}.jpg"))
                        {
                            byte[] buffer = new byte[4096];
                            int bytesRead;
                            do
                            {
                                bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                                outputStream.Write(buffer, 0, bytesRead);
                            } while (bytesRead != 0);
                        }
                    }
                    catch (Exception e)
                    {
                      //  _log.LogError(e.Message);
                       // _log.LogError(e.StackTrace);

                    }
                }

            }
        }
    }
}
