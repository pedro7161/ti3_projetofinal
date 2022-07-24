using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(barffood.Startup))]
namespace barffood
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
