using LMS.Domain.Entity;
using LMS.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.DTO.Create
{
    public  class ContractCreateModel
    {
   
        public Guid UserID { get; set; }
     
        public Guid TuitionFeeID { get; set; }
        public bool IsScholarship { get; set; }
    }
}
