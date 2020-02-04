using System.Collections.Generic;
using System.Net;

namespace Surfprize.Models.Core
{
    public sealed class UnauthorizedResponseModel : ResponseModel<List<Error>>
    {
        public UnauthorizedResponseModel(string message = null)
        {
            Status = HttpStatusCode.Unauthorized;
            Data = new List<Error>();
            if (!string.IsNullOrEmpty(message)) Data.Add(new Error("Message", message));
        }
    }
}
