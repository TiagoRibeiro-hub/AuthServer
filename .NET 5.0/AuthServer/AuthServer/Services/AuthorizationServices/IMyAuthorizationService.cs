using SportDataAuth.Responses;
using SportDataAuth.Shared.ApiResponses;
using SportDataAuth.Shared.AuthRequests;
using System;
using System.Threading.Tasks;

namespace SportDataAuth.Services.AdminServices
{
    public interface IMyAuthorizationService
    {
        Task<UserManagerResponse> AddRoleAsync(RoleRequests roleRequests);
    }
}
