using LMS.Application.DTO.Create;
using LMS.Application.DTO.Update;
using LMS.Application.Feature.Teacher.Command;
using LMS.Application.Feature.Teacher.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers;
//[Authorize]
//[ApiController]
//[Route("api/[controller]")]
public class TeacherController : Controller
{
    private readonly IMediator mediator;
    public TeacherController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpGet("GetTeacher")]
    public async Task<IActionResult> GetTeacher(Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new GetTeacherQueries(id));
        return Ok(res);
    }
    [HttpPut("CreateTeacher")]
   public async Task<IActionResult> CreateTeacher(TeacherCreateModel teacherCreateModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new CreateTeacherCommand(teacherCreateModel));
        return Ok(res);
    }
    [HttpPost("UpdateTeacher")]
    public async Task<IActionResult> UpdateTeacher(TeacherUpdateModel teacherUpdateModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new UpdateTeacherCommand(teacherUpdateModel));    
        return Ok(res);
    }
    [HttpDelete("DeleteTeacher")]
    public async Task<IActionResult> DeleteTeacher(Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new DeleteTeacherCommand(id));
        return Ok(res);
    }
}
