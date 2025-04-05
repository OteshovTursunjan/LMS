using LMS.Domain.Enum;
using LMS.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.DTO.Create
{
    public  class TeacherCreateModel 

    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Guid FacultyID { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Role2 Role2  { get; set; }
    }
}
