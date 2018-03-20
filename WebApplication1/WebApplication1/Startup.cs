using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebApplication1.Models;

[assembly: OwinStartupAttribute(typeof(WebApplication1.Startup))]
namespace WebApplication1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRoles();
        }

        private void createRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists("Customer"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }
        }
    }
}
