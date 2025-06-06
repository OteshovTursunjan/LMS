﻿using LMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.Entity
{
    public class Group : BaseEntity, IAuditedEntity
    {
        public string Name { get; set; }
        public string Tutor { get; set; }
        public Guid UserID  { get; set; }
        public string? CreatBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public string? UpdateBY { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
