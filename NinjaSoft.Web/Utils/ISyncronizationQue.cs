using System;
using System.Collections.Generic;
using NinjaSoft.Web.Models;
using NLog;

namespace NinjaSoft.Web.Utils
{
    public interface ISyncronizationQue
    {
        bool LegislatorsQueIsEmpty { get; }

        ILogger Log { get; set; }
        bool ContribuitorsQueIsEmpty { get; }
        DateTime ContribuitorsQueIsWaiting { get; set; }
        DateTime LegislatorsQueIsWaiting { get; set; }


        void AddToLegislatorsQue(Politician politician);

        Politician GetNextLegislator();

        void RemoveLegislatorFromQue(Politician policician);
        void AddToContriuitorsQue(List<Contributor> contributorslist);
        Contributor GetNextContributor();
        void RemoveContributorFromQue(Contributor contributor);
    }
}