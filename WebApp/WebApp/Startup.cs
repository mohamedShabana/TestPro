using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using WebApp.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApp.Startup))]
namespace WebApp
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUs();
        }
        public void CreateUs()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!roleManager.RoleExists("Adminstrator"))
            {
                var role = new IdentityRole();
                role.Name = "Adminstrator";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "adam";
                user.Email = "adam@pplus.com";
                string pass = "Java@123";
                var ch = UserManager.Create(user, pass);
                if (ch.Succeeded)
                {
                    var result = UserManager.AddToRole(user.Id, "Adminstrator");
                }
            }
            if (!roleManager.RoleExists("student"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "student";
                roleManager.Create(role);
            }
        }
    }
}
