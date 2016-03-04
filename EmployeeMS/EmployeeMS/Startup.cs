using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmployeeMS.Startup))]
namespace EmployeeMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
