using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.DTO.Update
{
    public  class TeacherUpdateModel
    {
        public Guid id {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Guid FacultyID { get; set; }
    }
}
