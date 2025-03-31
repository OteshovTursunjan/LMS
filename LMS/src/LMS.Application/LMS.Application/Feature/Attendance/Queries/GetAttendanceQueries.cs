using LMS.Application.DTO.Create;
using MediatR;


namespace LMS.Application.Feature.Attendance.Queries;

public record GetAttendanceQueries(Guid id) : IRequest<AttendanceCreateModel>;


