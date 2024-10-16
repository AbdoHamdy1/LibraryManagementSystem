using Microsoft.AspNetCore.Identity;

namespace LMS.Data
{
    public class Seed
    {

        private readonly RoleManager<IdentityRole> _roleManager;

        public Seed(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;

        }

    }
}
