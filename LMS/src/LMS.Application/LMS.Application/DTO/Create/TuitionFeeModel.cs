using LMS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.DTO.Create
{
    public  class TuitionFeeModel
    {
        public Guid FacultyId { get; set; }
        public int Summa { get; set; }
    }
}
