using LMS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.DTO.Update
{
    public  class TuitionFeeUpdateModel
    {
        public Guid id {  get; set; }
        public Faculty Faculty { get; set; }
        public Guid FacultyId { get; set; }
        public int Summa { get; set; }
    }
}
