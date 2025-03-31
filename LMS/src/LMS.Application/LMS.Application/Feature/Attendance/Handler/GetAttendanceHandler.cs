using LMS.Application.DTO.Create;
using LMS.Application.Feature.Attendance.Queries;
using LMS.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Feature.Attendance.Handler;

public  class GetAttendanceHandler : IRequestHandler<GetAttendanceQueries, AttendanceCreateModel>
{
    private readonly IAttendanceRepository attendanceRepository;
    public GetAttendanceHandler(IAttendanceRepository attendanceRepository)
    {
        this.attendanceRepository = attendanceRepository;
    }

    public async Task<AttendanceCreateModel> Handle(GetAttendanceQueries request, CancellationToken cancellationToken)
    {
        var attendance =await attendanceRepository.GetFirstAsync(u => u.id == request.id);
        if (attendance == null)
        {
            return null;
        }
        return new AttendanceCreateModel
        {
            IsAttendance = attendance.IsAttendance,
            UserID = attendance.AccountID,
            LessonsID = attendance.LessonsID,

        };
    }
}
