using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YourClassroom.Startup))]
namespace YourClassroom
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
