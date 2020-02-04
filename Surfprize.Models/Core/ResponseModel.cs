using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Surfprize.Models.Core
{
    public abstract class ResponseModel<TData>
    {
        public bool Success { get; set; } = true;
        public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;
        public TData Data { get; set; }
    }
}
