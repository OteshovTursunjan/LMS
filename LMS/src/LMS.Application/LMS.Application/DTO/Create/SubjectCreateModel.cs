using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.DTO.Create
{
    public  class SubjectCreateModel
    {
        public string Name { get; set; }
        public Guid FacultyID { get; set; }
    }
}
