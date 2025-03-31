using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.Subject.Command;

public record DeleteSubjectCommand(Guid id) : IRequest<bool>;
