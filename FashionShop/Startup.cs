using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FashionShop.Startup))]
namespace FashionShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
