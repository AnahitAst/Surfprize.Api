using System;
using System.Collections.Generic;
using System.Text;

namespace Surfprize.Models.Core
{
    public sealed class Error
    {
        public Error(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public string Key { get; set; }
        public string Message { get; set; }
    }
}
