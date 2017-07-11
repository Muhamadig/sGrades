using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(sGrades.Startup))]
namespace sGrades
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
