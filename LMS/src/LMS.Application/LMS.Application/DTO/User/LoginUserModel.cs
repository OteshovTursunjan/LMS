using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.DTO.User
{
    public  class LoginUserModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
