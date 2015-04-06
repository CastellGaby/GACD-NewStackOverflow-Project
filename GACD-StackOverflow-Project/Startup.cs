using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GACD_StackOverflow_Project.Startup))]
namespace GACD_StackOverflow_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
