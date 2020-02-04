using System;
using System.Collections.Generic;
using System.Text;

namespace Surfprize.Models.Core
{
    public sealed class ApiResponseModel<TData> : ResponseModel<TData>
    {
        public ApiResponseModel(TData data)
        {
            Data = data;
        }
    }
}
