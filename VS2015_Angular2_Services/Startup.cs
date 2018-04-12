using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(VS2015_Angular2_Services.Startup))]

namespace VS2015_Angular2_Services
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //Install-Package Microsoft.Owin.Cors
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureAuth(app);
        }
    }
}
