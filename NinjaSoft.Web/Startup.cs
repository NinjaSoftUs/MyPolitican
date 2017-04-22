using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin;
using NinjaSoft.Web.Migrations;
using NinjaSoft.Web.Models;
using NinjaSoft.Web.Utils;
using Owin;

[assembly: OwinStartupAttribute(typeof(NinjaSoft.Web.Startup))]
namespace NinjaSoft.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
          
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());



          

        }
    }
}
