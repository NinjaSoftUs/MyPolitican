using NinjaSoft.Web.Models;
using NinjaSoft.Web.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace NinjaSoft.Web.Controllers
{
    public class DbSyncController : ApiController
    {
        private bool _dbSyncIsRunning;
        private CancellationToken _token;
        private readonly CancellationTokenSource _cancellationTokenSource;

        private PoliticianRepository _repo;


        public DbSyncController()
        {
             _repo = new PoliticianRepository();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return null;
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
         

            _token = _cancellationTokenSource.Token;

            //legislators synce
            var getLegislatorsTask = new Task(() =>
            {
                var repo = new PoliticianRepository();
                while (!_token.IsCancellationRequested)
                {
                    var list = OpenSecretsApi.GetPolitician();
                    foreach (var politician in list)
                    {
                        repo.UpCertPolitician(politician);
                    }
                    Thread.Sleep(TimeSpan.FromDays(1));
                }
            }, _token);

            //legislators Summary synce
            var getCandSummaryTask = new Task(() =>
            {
                var repo = new PoliticianRepository();
                while (!_token.IsCancellationRequested)
                {
                    var cids = SyncronizationQue.LegislatorsQue;
                    if (!cids.Any())
                    {
                        foreach (var cid in repo.GetPoliticians().Select(x => x.Cid))
                        {
                            Thread.Sleep(TimeSpan
                                .FromMilliseconds(1)); //we need a delay so that the time stamp key will be uniqe
                            SyncronizationQue.AddToCandSummaryQue(cid);
                        }
                    }

                    foreach (var keyPair in SyncronizationQue.CandSummaryQue.ToList())
                    {
                        for (int i = 2012; i < DateTime.UtcNow.Year - 1; i++)
                        {
                            var summary = OpenSecretsApi.GetSummery(keyPair.Value.ToString(), i.ToString());
                            if (summary != null)
                            {
                                repo.UpCertSummary(summary);
                                SyncronizationQue.DeleteFromCandSummaryQue(summary);
                            }
                            else
                            {
                                Console.WriteLine();
                            }
                        }
                    }

                    Thread.Sleep(TimeSpan.FromDays(1.1));
                }
            }, _token);

            //Candate Contribuiton tabe
            var getCanContribTask = new Task(() =>
            {
              
                while (!_token.IsCancellationRequested)
                {
                    var cids = SyncronizationQue.GetCandCntribeQue();
                    if (!cids.Any())
                    {
                        cids = _repo.GetPoliticians().Select(x => x.Cid).ToList();
                        foreach (var cid in cids)
                        {
                            SyncronizationQue.AddToCandCntribeQue(cid);
                        }
                    }
                    foreach (var cid in cids)
                    {
                        for (int i = 1990; i < DateTime.Now.Year - 1; i++)
                        {
                            if (!SyncronizationQue.ContribuitorsQueIsWaiting)
                            {
                                var contribList = OpenSecretsApi.GetCandContrib(cid, i.ToString());
                                //var contribList = OpenSecretsApi.GetCandContrib(cid, "2016");
                                foreach (var contributor in contribList.ToList())
                                {
                                    _repo.UpCertContributor(contributor);
                                    SyncronizationQue.RemoveContributorFromQue(contributor);
                                }
                            }
                        }
                    }
                    Thread.Sleep(TimeSpan.FromDays(1.3));
                }

                
            }, _token);

            //  getLegislatorsTask.Start();
            // getCandSummaryTask.Start();
            getCanContribTask.Start();

            _dbSyncIsRunning = true;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}