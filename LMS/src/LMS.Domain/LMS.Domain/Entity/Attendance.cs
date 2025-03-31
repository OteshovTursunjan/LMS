using LMS.Domain.Common;
using LMS.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.Entity
{
    public  class Attendance : BaseEntity,IAuditedEntity
    { 
     public ApplicationUser User { get; set; }
    public Guid AccountID { get; set; }
    public Lesson Lessons { get; set; }
    public Guid LessonsID { get; set; }

    public bool IsAttendance { get; set; }

    public string? CreatBy { get; set; }
    public DateTime? CreatedOn { get; set; }

    public string? UpdateBY { get; set; }
    public DateTime? UpdatedOn { get; set; }
}
}
