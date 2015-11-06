using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ToDoListApp.Startup))]
namespace ToDoListApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
