using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProFinanciacion.Startup))]
namespace ProFinanciacion
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
