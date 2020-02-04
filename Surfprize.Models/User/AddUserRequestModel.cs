using Surfprize.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surfprize.Models.User
{
    public class AddUserRequestModel
    {
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}
