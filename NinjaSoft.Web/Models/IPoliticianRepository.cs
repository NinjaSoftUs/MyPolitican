using System.Collections.Generic;

namespace NinjaSoft.Web.Models
{
    public interface IPoliticianRepository
    {
        IEnumerable<Politician> GetPoliticians();
        Politician GetPoliticianById(int id);
        Politician UpCertPolitician(Politician politician);
      
        bool DeletePolitician(int id);
 
        Contributor UpCertContributor(Contributor sponcer);
        Contributor FindContributorByNameAndCid(string orgName, string cid);
        Politician FindPoliticianByCid(string cid);


        Summary UpCertSummary(Summary summery);
        IEnumerable<string> GetContributerList();
        string FindCidByName(string policiticanName);
    }
}
