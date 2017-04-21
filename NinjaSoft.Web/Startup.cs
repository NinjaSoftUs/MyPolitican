using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NinjaSoft.Web.Startup))]
namespace NinjaSoft.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
