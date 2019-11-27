using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JustBlog.Startup))]
namespace JustBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
