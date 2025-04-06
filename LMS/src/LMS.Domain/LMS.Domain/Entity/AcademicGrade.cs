using LMS.Domain.Common;
using LMS.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.Entity;

public  class AcademicGrade : BaseEntity, IAuditedEntity
{
    public Subject Subject { get; set; }
    public Guid SubjectId { get; set; }
    public ApplicationUser Account { get; set; }
    public Guid AccountID { get; set; }
    public int CurrentGrade { get; set; } = 0;
    public int MidTerm { get; set; } = 0;
    public int FinaleExam { get; set; } = 0;
    public int OverallGrades { get; set; } = 0;
    public bool IsFail { get; set; } = false;
    public string? CreatBy { get; set; }
    public DateTime? CreatedOn { get; set; }

    public string? UpdateBY { get; set; }
    public DateTime? UpdatedOn { get; set; }
}
