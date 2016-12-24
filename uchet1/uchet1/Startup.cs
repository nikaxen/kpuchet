using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(uchet1.Startup))]
namespace uchet1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
