using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Elearning.Website.Startup))]
namespace Elearning.Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
