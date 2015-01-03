using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(nmct.ssa.cashlessproject.web.Startup))]
namespace nmct.ssa.cashlessproject.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
