using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UpdateMe.Startup))]
namespace UpdateMe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
