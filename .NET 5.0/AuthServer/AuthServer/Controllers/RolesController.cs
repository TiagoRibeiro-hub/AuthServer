using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportDataAuth.Responses;
using SportDataAuth.Services.AdminServices;
using SportDataAuth.Shared.ApiResponses;
using SportDataAuth.Shared.AuthRequests;
using System.Linq;
using System.Threading.Tasks;

namespace SportDataAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMyAuthorizationService _myAuthorizationService;

        public RolesController(IMyAuthorizationService myAuthorizationService)
        {
            _myAuthorizationService = myAuthorizationService;
        }

        // api/roles/add
        [HttpPost("add")]
        [ProducesResponseType(200, Type = typeof(ApiResponse))]
        [ProducesResponseType(400, Type = typeof(ApiResponseError))]
        public async Task<ApiResponse> AddRoleAsync([FromBody] RoleRequests roleRequests)
        {
            var result = await _myAuthorizationService.AddRoleAsync(roleRequests);

            if (result.IsSucess)
                return result.AsApiResponseDto();

            return result.AsApiResponseErrorDto();

        }
    }
}
