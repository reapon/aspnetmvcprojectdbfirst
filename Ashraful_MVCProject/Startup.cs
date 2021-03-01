using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ashraful_MVCProject.Startup))]
namespace Ashraful_MVCProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
