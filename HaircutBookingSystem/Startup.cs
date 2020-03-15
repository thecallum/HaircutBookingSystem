using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HaircutBookingSystem.Startup))]
namespace HaircutBookingSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
