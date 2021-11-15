using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportDataAuth.Shared.ApiResponses
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public string UrlClientPage { get; set; }
        public ApiResponse(string message)
        {
            Message = message;
            IsSuccess = true;
        }

        public ApiResponse()
        {
            IsSuccess = true;
        }
    }
}
