using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DL_Bank.Startup))]
namespace DL_Bank
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
