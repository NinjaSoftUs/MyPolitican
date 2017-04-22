using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin.Logging;

namespace NinjaSoft.Web.Models
{
    public class DbInitializer
    {
        private static IPoliticianRepository _repository;
        private static ILogger _log;
       // private static ISyncronizationQue _syncronizationQue;
       

        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            _log = applicationBuilder.ApplicationServices
                     .GetRequiredService<ILoggerFactory>().CreateLogger("DbInitializer");

            _repository = applicationBuilder.ApplicationServices
                .GetRequiredService<IPoliticianRepository>();

            _syncronizationQue = applicationBuilder.ApplicationServices
                .GetRequiredService<ISyncronizationQue>();

            _syncronizationQue.Log = _log;

            OpenSecretsApi.SyncroizationQue = _syncronizationQue;
            OpenSecretsApi.Log = _log;


            _log.LogInformation("DbInitializer Seed called");

            
            SyncPoliticians();
            SyncContribuitors();
            //  SyncSummary();
        }

        private static void SyncContribuitors()
        {

            //fill up que
            if (_syncronizationQue.ContribuitorsQueIsEmpty && DateTime.UtcNow >= _syncronizationQue.ContribuitorsQueIsWaiting)
            {
                var cidList = (from items in _repository.GetPoliticians()
                    select items.Cid).ToList();

                List<Contributor> contributorslist = new List<Contributor>();

                for (int i = 2012; i < DateTime.UtcNow.Year -1; i++)
                {
                    contributorslist.AddRange(OpenSecretsApi.GetCandContrib(cidList,i.ToString()));
                }


                _syncronizationQue.AddToContriuitorsQue(contributorslist);

            }

            //now process que

            while (!_syncronizationQue.ContribuitorsQueIsEmpty)
            {
                var contributor = _syncronizationQue.GetNextContributor();
                if (contributor != null)
                {
                    _repository.UpCertContributor(contributor);
                    _syncronizationQue.RemoveContributorFromQue(contributor);
                }

            }

        }

        private static void SyncSummary()
        {
            foreach (var politician in _repository.GetPoliticians().ToList())
            {
                for (int i = 2012; i < DateTime.UtcNow.Year; i++)
                {
                    var summery = OpenSecretsApi.GetSummery(politician.Cid, i.ToString());
                    if (summery != null)
                    {
                        _repository.UpCertSummary(summery);
                    }
                }
            }
        }

        private static void SyncPoliticians()
        {
            if (_syncronizationQue.LegislatorsQueIsEmpty && DateTime.UtcNow >= _syncronizationQue.LegislatorsQueIsWaiting)
            {
               
                var legislators = OpenSecretsApi.GetPolitician();

                foreach (var politician in legislators)
                {
                    _syncronizationQue.AddToLegislatorsQue(politician);
                }
            }

            while (!_syncronizationQue.LegislatorsQueIsEmpty )
            {
                var policician = _syncronizationQue.GetNextLegislator();
                if (policician != null)
                {
                    _repository.UpCertPolitician(policician);
                    _syncronizationQue.RemoveLegislatorFromQue(policician);
                }
            }
        }

        private static void UpdateContributers(Politician politician)
        {
            if (politician.LastUpdate <=
                DateTime.UtcNow.Subtract(TimeSpan.FromDays(3))
            ) //weare going to throtal web request (only allowed 200 a day)
            {
                var list = OpenSecretsApi.GetCandContrib(politician.Cid, (DateTime.Now.Year - 1).ToString());

                foreach (var contributor in list)
                {
                    if (_repository.FindContributorByNameAndCid(contributor.OrgName, contributor.Cid) == null)
                    {
                        _repository.UpCertContributor(contributor);
                        politician.LastUpdate = DateTime.UtcNow;
                        _repository.UpCertPolitician(politician); //reset time stamp so we do not pull data for 3 more days
                    }
                }
            }
        }
    }
}