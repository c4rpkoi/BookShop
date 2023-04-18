using Microsoft.AspNetCore.Identity;

namespace Assignment.Data
{
    public class DbInitializer
    {
        public static void Initialize(ShopContext context, IServiceProvider service)
        {
            context.Database.EnsureCreated();

            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = service.GetRequiredService<UserManager<IdentityUser>>();

            if (context.Books.Any())
            {
                return;
            }
            CreateAdminRole(context, roleManager, userManager);

        }

        private static void CreateAdminRole(ShopContext context, RoleManager<IdentityRole> _roleManager, UserManager<IdentityUser> _userManager)
        {
            bool roleExists = _roleManager.RoleExistsAsync("Admin").Result;
            if (roleExists)
            {
                return;
            }
            var role = new IdentityRole()
            {
                Name = "Admin"
            };
            _roleManager.CreateAsync(role).Wait();

            var user = new IdentityUser()
            {
                UserName = "admin",
                Email = "admin@default.com"
            };
            string adminPassword = "Password123";
            var userResult = _userManager.CreateAsync(user, adminPassword).Result;
            if (userResult.Succeeded) 
            {
                _userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }
}
