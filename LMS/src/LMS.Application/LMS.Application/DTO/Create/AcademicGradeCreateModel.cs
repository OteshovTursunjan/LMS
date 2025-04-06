using LMS.Domain.Entity;
using LMS.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.DTO.Create
{
    public  class AcademicGradeCreateModel
    {
      
        public Guid SubjectId { get; set; }
     
        public Guid UserId { get; set; }
        public int CurrentGrade { get; set; }
        public int MidTerm { get; set; } = 0;
        public int FinaleExam { get; set; } = 0;
      
    }
}
