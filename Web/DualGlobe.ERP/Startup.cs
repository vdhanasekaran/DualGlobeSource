using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DualGlobe.ERP.Startup))]
namespace DualGlobe.ERP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
