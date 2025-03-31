using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.DTO.Update
{
    public  class SubjectUpdateModel
    {
        public Guid id {  get; set; }
        public string Name { get; set; }
        public Guid FacultyID { get; set; }
    }
}
