using LMS.Application.Feature.Attendance.Command;
using LMS.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.Attendance.Handler;

public class CreateAttendanceHandler : IRequestHandler<CreateAttendanceCommand, bool>
{
    public readonly IAttendanceRepository _attendanceRepository;
    public CreateAttendanceHandler(IAttendanceRepository attendanceRepository)
    {
        _attendanceRepository = attendanceRepository;
    }
    public async Task<bool> Handle(Command.CreateAttendanceCommand request, CancellationToken cancellationToken)
    {
        var attendance = new LMS.Domain.Entity.Attendance()
        {
            AccountID = request.AttendanceCreateModel.UserID,
            LessonsID = request.AttendanceCreateModel.LessonsID,
            IsAttendance = request.AttendanceCreateModel.IsAttendance,
        };
        await _attendanceRepository.AddAsync(attendance);
        return true;
    }
}
