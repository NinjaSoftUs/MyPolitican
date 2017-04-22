using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Owin.Logging;

namespace NinjaSoft.Web.Models
{
    public  class PoliticianRepository
    {
        private  ApplicationDbContext _applicationDbContext;
        public   ILogger Log { get; set; }

         public  PoliticianRepository()
        {
            _applicationDbContext = new ApplicationDbContext();

            Mapper.Initialize(config =>
            {
                config.CreateMap<Politician, Politician>().ForMember(x => x.Id, y => y.Ignore()).ForMember(x => x.CreateDate, y => y.Ignore());
                config.CreateMap<Sector, Sector>().ForMember(x => x.Id, y => y.Ignore()).ForMember(x => x.CreateDate, y => y.Ignore());
                config.CreateMap<Contributor, Contributor>().ForMember(x => x.Id, y => y.Ignore()).ForMember(x => x.CreateDate, y => y.Ignore());
            });

            //  _log = loggerFactory.CreateLogger("PoliticianRepository");
        }

        public  IEnumerable<Politician> GetPoliticians()
        {
          //  _log.LogInformation("GetPoliticains Called");
            try
            {
                var results = from items in _applicationDbContext.Politicians
                    orderby items.Firstlast
                    select items;
                return results;
            }
            catch (Exception e)
            {
               Console.WriteLine(e);
                return null;
            }
        }

        public  Politician GetPoliticianById(int id)
        {
           // _log.LogInformation($"GetPoliticianById Called wtih id={id}");
            try
            {
                var results = (from items in _applicationDbContext.Politicians
                    where items.Id == id
                    select items).FirstOrDefault();
                return results;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public  Politician UpCertPolitician(Politician politician)
        {
            var d1 = politician.Birthdate;
            var d2 = politician.CreateDate;
            var d3 = politician.LastUpdate;
          //  _log.LogInformation($"UpCertPolitician Called Politician Cid={politician.Cid}");
            try
            {
                var tmpPol = FindPoliticianByCid(politician.Cid);
                if (tmpPol != null)
                {
               
                    Mapper.Map<Politician,Politician>(source:politician, destination:tmpPol);
                    tmpPol.LastUpdate = DateTime.UtcNow;
                    _applicationDbContext.Politicians.Add(tmpPol);
                    politician.Id = tmpPol.Id;
                }
                else
                {

                    _applicationDbContext.Politicians.Add(politician);
                }


                _applicationDbContext.SaveChanges();

                return GetPoliticianById(politician.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public  Politician FindPoliticianByCid(string cid)
        {
            try
            {
              

                    //_log.LogInformation($"FindPoliticianByCid Called Politician Cid={cid}");
                    var target = (from items in _applicationDbContext.Politicians
                        where items.Cid == cid
                        select items).FirstOrDefault();

                    return target;
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public  Summary UpCertSummary(Summary summery)
        {
            try
            {
                var target = (from items in _applicationDbContext.Summarys
                    where items.Cid == summery.Cid && items.Cycle == summery.Cycle
                    select items).FirstOrDefault();

                if (target != null)
                {
                    Mapper.Map(summery, target);
                    target.LastUpdate =DateTime.UtcNow;
                    _applicationDbContext.Summarys.Add(target);
                    summery.Id = target.Id;
                }
                else
                {
                    _applicationDbContext.Summarys.Add(summery);
                }
                _applicationDbContext.SaveChanges();
                return summery;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public  IEnumerable<string> GetContributerList()
        {
            try
            {
                var list = _applicationDbContext.Contributors.GroupBy(x => x.OrgName)
                    .Select(grp => grp.First()).ToList();

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }

        public  string FindCidByName(string policiticanName)
        {
            try
            {
                var split = policiticanName.Split(' ');
                var lastName = split.LastOrDefault();
                if (lastName != null)
                {
                    var target = (from items in _applicationDbContext.Politicians
                        where items.Lastname == lastName.ToUpper()
                        select items.Cid).FirstOrDefault();
                    return target;

                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }


        public  bool DeletePolitician(int id)
        {
            try
            {
                // _log.LogInformation($"DeletePolitician Called id={id}");
                var target = GetPoliticianById(id);
                _applicationDbContext.Politicians.Remove(target);
                _applicationDbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

    
        public  Contributor UpCertContributor(Contributor contributor)
        {
            try
            {
                //  _log.LogInformation($"UpCertContributor Called OrgName={contributor.OrgName}");
                var targetContributor = FindContributor(contributor);
                if (targetContributor != null)
                {
                    Mapper.Map(contributor, targetContributor);
                    _applicationDbContext.Contributors.Add(targetContributor);
                    _applicationDbContext.SaveChanges();
                    return contributor;
                }
                else
                {
                    _applicationDbContext.Contributors.Add(contributor);
                    _applicationDbContext.SaveChanges();
                    return contributor;

                    //_applicationDbContext.Contributor.Add(new Contributor
                    //{
                    //    Cid = contributor.Cid,
                    //    OrgName = contributor.OrgName,
                    //    Total = contributor.Total,
                    //    Cycle = contributor.Cycle,
                    //    Indivs = contributor.Indivs,
                    //    LogoUrl = contributor.LogoUrl,
                    //    Notice = contributor.Notice,
                    //    Origin = contributor.Origin,
                    //    Pacs = contributor.Pacs,
                    //    Source = contributor.Source,
                    //    LastUpdate = contributor.LastUpdate
                    //});
                }
               


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private Contributor FindContributor(Contributor contributor)
        {
            try
            {
                var target = (from items in _applicationDbContext.Contributors
                    where items.Cid == contributor.Cid &&
                          items.Cycle == contributor.Cycle &&
                          items.OrgName == contributor.OrgName
                    select items).FirstOrDefault();

                return target;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
               // throw;
                return null;
            }
        }

        public  Contributor FindContributorByNameAndCid(string orgName,string cid)
        {
            try
            {
                // _log.LogInformation($"FindContributorByNameAndCid Called OrgName={orgName} cid={cid}");

                var result = (from items in _applicationDbContext.Contributors
                    where items.Cid == cid && items.OrgName == orgName
                    select items).FirstOrDefault();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        
    }
}
