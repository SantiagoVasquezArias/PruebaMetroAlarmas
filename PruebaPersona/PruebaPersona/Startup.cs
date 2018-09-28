using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PruebaPersona.Startup))]
namespace PruebaPersona
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
