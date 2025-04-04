using LMS.Application.DTO.Create;
using LMS.Application.DTO.Update;
using LMS.Application.Feature.Lesson.Command;
using LMS.Application.Feature.Lesson.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers;

public class LessonController : Controller
{
    private readonly IMediator mediator;
    public LessonController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpGet("GetLesson")]
    public async Task<IActionResult> GetLesson(Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new GetLessonQueries(id));
        return Ok(res);
    }
    [HttpPut("CreateLesson")]
    public async Task<IActionResult> CreateLesson(LessonCreateModel lessonCreateModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new CreateLessonCommand(lessonCreateModel));
        return Ok(res);
    }
    [HttpPost("UpdateLesson")]
    public async Task<IActionResult> UpdateLesson(LessonUpdateModel lessonUpdateModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new  UpdateLessonCommand(lessonUpdateModel));
        return Ok(res);
    }
    [HttpDelete("DeleteLesson")]
    public async Task<IActionResult> DeleteLesson(Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new DeleteLessonCommand(id));
        return Ok(res);
    }
}
