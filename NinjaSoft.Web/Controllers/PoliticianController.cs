using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using NinjaSoft.Web.Models;
using NinjaSoft.Web.Utils;

namespace NinjaSoft.Web.Controllers
{

    public class PoliticianController : ApiController
    {
       

     
     

        public PoliticianController()
        {
           
        
           
        }
      
     

    
        public void Post(string flag)
        {
         

            

        }

        public IEnumerable<Politician> Get()
        {
           // var list = .GetPoliticians();

            //var result = new JsonResult();
            //result.Data = list.OrderBy(x => x.Lastname);

            //if (result.Value == null)
            //{
            //    return NotFound();
            //}

           // return list.OrderBy(x => x.Lastname); 
            return null;
        }
    }
}
