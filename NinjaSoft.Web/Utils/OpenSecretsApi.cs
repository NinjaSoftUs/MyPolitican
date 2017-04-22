using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Xml;
using System.Xml.Linq;
using NinjaSoft.Web.Models;
using NLog;

namespace NinjaSoft.Web.Utils
{
    public static class OpenSecretsApi
    {
        private static readonly HttpClient _client = new HttpClient();

      //  private static IApplicationBuilder _applicationBuilder;

        private const string BASEURL = "http://www.opensecrets.org/api/?method=";
        private const string APIKEY = "58bf9a332e5f118dfec53bd2c0f7665b";

        private static List<string> _sateCodes { get; } = new List<string>
        {
            "AL",
            "AK",
            "AZ",
            "AR",
            "CA",
            "CO",
            "CT",
            "DE",
            "FL",
            "GA",
            "HI",
            "ID",
            "IL",
            "IN",
            "IA",
            "KS",
            "KY",
            "LA",
            "ME",
            "MD",
            "MA",
            "MI",
            "MN",
            "MS",
            "MO",
            "MT",
            "NE",
            "NV",
            "NH",
            "NJ",
            "NM",
            "NY",
            "NC",
            "ND",
            "OH",
            "OK",
            "OR",
            "PA",
            "RI",
            "SC",
            "SD",
            "TN",
            "TX",
            "UT",
            "VT",
            "VA",
            "WA",
            "WV",
            "WI",
            "WY"
        };

        public static ISyncronizationQue SyncroizationQue { get; set; }

        public static ILogger Log { get; set; }

        #region getLegislators

        public static List<Politician> GetPolitician()
        {
           // Log.LogInformation($"OpenSecretsApi-GetPolitician called");

            var list = new List<Politician>();

            try
            {
                var callLimitError = false;
                foreach (var sateCode in _sateCodes)
                {
                    if (callLimitError)
                    {
                       // Log.LogWarning("Call Limit for getLegislators hit (try again tomarrow)");
                      //  Log.LogWarning("Putting LegislatorsQue into a wating state");
                        SyncronizationQue.LegislatorsQueIsWaiting = true;
                        break;
                    }
                    string method = "getLegislators";
                    string url = $"{BASEURL}{method}&id={sateCode}&apikey={APIKEY}";

                    var aTask = _client.GetAsync(url)
                        .ContinueWith(r =>
                            {
                                r.Result.Content.ReadAsStringAsync()
                                    .ContinueWith(x =>
                                    {
                                        if (x.Result == "call limit has been reached")
                                        {
                                            callLimitError = true;
                                        }
                                        else
                                            list.AddRange(GetPolitician(x.Result));
                                    }).Wait();
                            }
                        );

                    aTask.Wait();
                }
            }
            catch (Exception e)
            {
               // Log.LogError(e.Message, e);
              //  Log.LogError(e.StackTrace);
            }
            return list;
        }

        private static IEnumerable<Politician> GetPolitician(string xmlResult)
        {
          //  Log.LogInformation($"OpenSecretsApi-GetPolitician called");

            var list = new List<Politician>();
            try
            {
                var xDoc = XDocument.Parse(xmlResult);
                foreach (var legislator in xDoc.Descendants("legislator"))
                {
                    var politician = new Politician();
                    politician.Cid = legislator.Attribute("cid")?.Value;
                    politician.Firstlast = legislator.Attribute("firstlast")?.Value;
                    politician.Lastname = legislator.Attribute("lastname")?.Value;
                    politician.Party = legislator.Attribute("party")?.Value;
                    politician.Office = legislator.Attribute("office")?.Value;
                    politician.Gender = legislator.Attribute("gender")?.Value;
                    politician.FirstElected = legislator.Attribute("first_elected")?.Value;
                    politician.ExitCode = legislator.Attribute("exit_code")?.Value;
                    politician.Comments = legislator.Attribute("comments")?.Value;
                    politician.Phone = legislator.Attribute("phone")?.Value;
                    politician.Fax = legislator.Attribute("fax")?.Value;
                    politician.Website = legislator.Attribute("website")?.Value;
                    politician.CongressOffice = legislator.Attribute("congress_office")?.Value;
                    politician.BioguideId = legislator.Attribute("bioguide_id")?.Value;
                    politician.VotesmartId = legislator.Attribute("votesmart_id")?.Value;
                    politician.Feccandid = legislator.Attribute("feccandid")?.Value;
                    politician.TwitterId = legislator.Attribute("twitter_id")?.Value;
                    politician.YoutubeUrl = legislator.Attribute("youtube_url")?.Value;
                    politician.FacebookId = legislator.Attribute("facebook_id")?.Value;
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(legislator.Attribute("birthdate")?.Value))
                            politician.Birthdate = Convert.ToDateTime(legislator.Attribute("birthdate")?.Value);
                    }
                    catch (Exception e)
                    {
                      //  Log.LogError(e.Message, e);
                      //  Log.LogError($"Can not parse birthdate from webserice call value={legislator.Attribute("birthdate")?.Value}");
                       // Log.LogError(e.StackTrace);
                    }
                    list.Add(politician);
                }
            }
            catch (Exception ex)
            {
              //  Log.LogError(ex.Message, ex);
              //  Log.LogError(ex.StackTrace);
            }
            return list;
        }

        #endregion getLegislators

       

        public static IEnumerable<Contributor> GetCandContrib(string cid, string cycle)
        {
          //  Log.LogInformation($"OpenSecretsApi-GetCandContrib called cid={cid},cycle={cycle}");

            List<Contributor> list = new List<Contributor>();
            try
            {
                if (!SyncronizationQue.ContribuitorsQueIsWaiting)
                {
                    string method = "candContrib";
                    string url = $"{BASEURL}{method}&cid={cid}&cycle={cycle}&apikey={APIKEY}";

                    var aTask = _client.GetAsync(url)
                        .ContinueWith(r =>
                            {
                                r.Result.Content.ReadAsStringAsync()
                                    .ContinueWith(response =>
                                    {
                                        if (response.Result == "call limit has been reached")
                                        {
                                         //   Log.LogWarning("Call Limit for candContrib hit (try again tomarrow)");
                                           // Log.LogWarning("Putting ContribuitorsQueIsWaiting into a wating state");
                                            SyncronizationQue.ContribuitorsQueIsWaiting = true;
                                        }
                                        else
                                        {
                                            try
                                            {
                                                var xdoc = XDocument.Parse(response.Result);
                                                list.AddRange(GetCandContrib(cid,  xdoc));
                                            }
                                            catch (Exception e)
                                            {
                                             //   Log.LogError($"Unable to get data for contributor cid={cid} cyle={cycle}\nURL={url}\nResuls={response.Result}");
                                             //   Log.LogError(e.Message);
                                             //   Log.LogError(e.StackTrace);
                                            }
                                         
                                        }
                                    })
                                    .Wait();
                            }
                        );

                    aTask.Wait();
                }
            }
            catch (Exception e)
            {
               // Log.LogError(e.Message, e);
               // Log.LogError(e.StackTrace, e);
            }

            return list;
        }

        private static IEnumerable<Contributor> GetCandContrib(string cid,  XDocument xDoc)
        {
            var list = new List<Contributor>();

            try
            {
                var origin = xDoc.Descendants("contributors").First().Attribute("origin").Value;
                var source = xDoc.Descendants("contributors").First().Attribute("source").Value;
                var notice = xDoc.Descendants("contributors").First().Attribute("notice").Value;
                var cycle = xDoc.Descendants("contributors").First().Attribute("cycle").Value;
       
                foreach (var sponser in xDoc.Descendants("contributor"))
                {
                    var contributor = new Contributor
                    {
                        Cid = cid,
                        Origin = origin,
                        Source = source,
                        Notice = notice,
                        Cycle = cycle,
                        Indivs = Convert.ToInt32(sponser.Attribute("indivs").Value),
                        Pacs = Convert.ToInt32(sponser.Attribute("pacs").Value),
                        Total = Convert.ToInt32(sponser.Attribute("total").Value),
                        OrgName = sponser.Attribute("org_name").Value,
                        LastUpdate = DateTime.UtcNow
                    };
                    list.Add(contributor);
                }
            }
            catch (Exception e)
            {
              //  Log.LogError(e.Message);
              //  Log.LogError(e.StackTrace);
            }

            return list;
        }

        //public static List<object> GetOrgs()
        //{
        //    var url = "http://www.opensecrets.org/api/?method=getOrgs&id=" + "&apikey=" + APIKEY;

        //    var aTask = _client.GetAsync(url)
        //        .ContinueWith(r =>
        //            {
        //                r.Result.Content.ReadAsStringAsync()
        //                    .ContinueWith(x =>
        //                    {
        //                       Console.WriteLine(x);
        //                    }).Wait();

        //            }
        //        );

        //    aTask.Wait();
        //    return null;
        //}

        public static Summary GetSummery(string cid, string cycle)
        {
            Summary summary = null;
            var url = $"{BASEURL}candSummary&cid={cid}&cycle={cycle}&apikey={APIKEY}";

            var aTask = _client.GetAsync(url)
                .ContinueWith(r =>
                    {
                        r.Result.Content.ReadAsStringAsync()
                            .ContinueWith(x =>
                            {
                                try
                                {
                                    var xDoc = XDocument.Parse(x.Result);
                                    summary = CreateSummery(xDoc);
                                }
                                catch (XmlException)
                                {
                                  //  Log.LogWarning("Call to appi with method candSummary faild");
                                  //  Log.LogWarning(x.Result);
                                }
                            }).Wait();
                    }
                );

            aTask.Wait();

            return summary;
        }

        private static Summary CreateSummery(XDocument xml)
        {
            try
            {
                var summery = new Summary
                {
                    Cycle = xml.Descendants("summary").First().Attribute("cycle").Value,
                    Cid = xml.Descendants("summary").First().Attribute("cid").Value,
                    CashOnHand = Convert.ToDouble(xml.Descendants("summary").First().Attribute("cash_on_hand").Value),
                    Debt = Convert.ToDouble(xml.Descendants("summary").First().Attribute("debt").Value),
                    FirstElected = xml.Descendants("summary").First().Attribute("first_elected").Value,
                    NextElection = xml.Descendants("summary").First().Attribute("next_election").Value,
                    CreateDate = DateTime.UtcNow,
                    LastUpdate = DateTime.UtcNow,
                    Origin = xml.Descendants("summary").First().Attribute("origin").Value,
                    Source = xml.Descendants("summary").First().Attribute("source").Value,
                    SourceLastUpdated = xml.Descendants("summary").First().Attribute("last_updated").Value,
                    Spent = Convert.ToDouble(xml.Descendants("summary").First().Attribute("spent").Value),
                    Total = Convert.ToDouble(xml.Descendants("summary").First().Attribute("total").Value)
                };

                return summery;
            }
            catch (Exception e)
            {
               // Log.LogError(e.Message);
               // Log.LogError(e.StackTrace);
                return null;
            }
        }

        public static IEnumerable<string> GetCanSummary(string cid, int i)
        {
            throw new NotImplementedException();
        }
    }
}