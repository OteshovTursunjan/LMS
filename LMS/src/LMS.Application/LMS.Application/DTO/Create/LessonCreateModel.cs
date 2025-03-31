using LMS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.DTO.Create
{
    public  class LessonCreateModel
    {
        public Subject Subject { get; set; }
        public Guid SubjcetID { get; set; }

        public Teacher Teachers { get; set; }
        public Guid TeacherID { get; set; }

        public required DateTime LessonTime { get; set; }
        public string Room { get; set; }
        public Group Groups { get; set; }
        public Guid GroupID { get; set; }

    }
}
