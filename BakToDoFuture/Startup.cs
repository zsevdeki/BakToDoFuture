using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BakToDoFuture.Startup))]
namespace BakToDoFuture
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
