using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GymNotes.Startup))]
namespace GymNotes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
