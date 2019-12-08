using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LawCMS.Startup))]
namespace LawCMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
