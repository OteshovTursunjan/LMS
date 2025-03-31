using LMS.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.DTO.User
{
    public  class CustomClaim
    {
        public const string Email = "email";
        public const Roles Role = Roles.Admin;
        public const string Id = "id";
        public const string Name = "name";
    }
}
