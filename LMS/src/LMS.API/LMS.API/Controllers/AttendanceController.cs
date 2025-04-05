using LMS.Application.DTO.Create;
using LMS.Application.Feature.Attendance.Command;
using LMS.Application.Feature.Attendance.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers;

public class AttendanceController : Controller
{
   private readonly IMediator mediator;
  
    public AttendanceController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpGet("GetAttendance")]
    public async Task<IActionResult> GetAttendance(Guid AttendanceId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new GetAttendanceQueries(AttendanceId));
        return Ok(res);
    }
    [HttpPost("CreateAttendance")]
    public async Task<IActionResult> CreateAttendance(AttendanceCreateModel attendanceCreateModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new CreateAttendanceCommand(attendanceCreateModel));
        return Ok(res);
    }
}
