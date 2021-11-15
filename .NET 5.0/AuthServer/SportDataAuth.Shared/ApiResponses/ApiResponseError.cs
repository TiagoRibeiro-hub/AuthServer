using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportDataAuth.Shared.ApiResponses
{
    public class ApiResponseError : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }

        public ApiResponseError()
        {
            IsSuccess = false;
        }

        public ApiResponseError(IEnumerable<string> error)
        {
            IsSuccess = false;
            Errors = error;
        }

        public ApiResponseError(IEnumerable<string> error, string message)
        {
            Message = message;
            IsSuccess = false;
            Errors = error;
        }
    }
}
