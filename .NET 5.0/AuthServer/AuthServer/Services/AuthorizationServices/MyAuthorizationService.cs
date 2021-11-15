using Microsoft.AspNetCore.Identity;
using SportDataAuth.Responses;
using SportDataAuth.Shared.AuthRequests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SportDataAuth.Services.AdminServices
{
    public class MyAuthorizationService : IMyAuthorizationService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public MyAuthorizationService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // ADD ROLE
        public async Task<UserManagerResponse> AddRoleAsync(RoleRequests roleRequests)
        {
            if (roleRequests is null)
                return new UserManagerResponse
                {
                    Message = "Role is null.",
                    IsSucess = false,
                };

            if (await _roleManager.RoleExistsAsync(roleRequests.Role))
                return new UserManagerResponse
                {
                    Message = "Role already exist.",
                    IsSucess = false,
                };

            var role = new IdentityRole()
            {
                Name = roleRequests.Role,
            };

            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "Role created succesfully!",
                    IsSucess = true,
                };

            return new UserManagerResponse
            {
                Message = "Role did not create",
                IsSucess = false,
                Errors = result.Errors.Select(x => x.Description),
            };

        }
    }
}
