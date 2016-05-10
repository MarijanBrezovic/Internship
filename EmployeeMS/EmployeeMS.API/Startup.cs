using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Serilog;

[assembly: OwinStartup(typeof(EmployeeMS.API.Startup))]

namespace EmployeeMS.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Log.Logger = new LoggerConfiguration()
                            .WriteTo.RollingFile(AppDomain.CurrentDomain.BaseDirectory + "Logs\\Log-{Date}.txt")
                            .CreateLogger();
        }
    }
}
