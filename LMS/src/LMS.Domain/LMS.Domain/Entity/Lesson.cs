using LMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LMS.Domain.Entity
{
    public  class Lesson : BaseEntity, IAuditedEntity
    {
        public Subject Subject { get; set; }
        public Guid SubjcetID { get; set; }

        public Teacher Teachers { get; set; }
        public Guid TeacherID { get; set; }

        public required DateTime LessonTime { get; set; }
        public string Room { get; set; }
        public Group Groups { get; set; }
        public Guid GroupID { get; set; }


        public string? CreatBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public string? UpdateBY { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
