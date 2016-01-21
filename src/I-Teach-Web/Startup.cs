using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(I_Teach_Web.Startup))]
namespace I_Teach_Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
