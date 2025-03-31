using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.Exceptions
{
    [Serializable]
    public  class ResourceNotFound : Exception
    {
        public ResourceNotFound() { }

        public ResourceNotFound(Type type) : base($"{type} is missing") { }

        protected ResourceNotFound(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public ResourceNotFound(string? message) : base(message) { }

        public ResourceNotFound(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
