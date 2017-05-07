using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InterventionManagementSystem_MVC.Startup))]
namespace InterventionManagementSystem_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            //run setup script
        }
    }
}
