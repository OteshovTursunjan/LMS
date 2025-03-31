using LMS.Domain.Entity;
using LMS.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.DTO.Create
{
    public  class AttendanceCreateModel
    {
     
        public Guid UserID { get; set; }
     
        public Guid LessonsID { get; set; }

        public bool IsAttendance { get; set; }
    }
}
