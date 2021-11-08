using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineWebApp.Startup))]
namespace OnlineWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
