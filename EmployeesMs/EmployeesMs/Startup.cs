using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmployeesMs.Startup))]
namespace EmployeesMs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
