using LMS.Application.DTO.Create;
using LMS.Application.DTO.Update;
using LMS.Application.Feature.Subject.Command;
using LMS.Application.Feature.Subject.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers;
[Authorize(Policy = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class SubjectController : Controller
{
    private readonly IMediator mediator;
    public SubjectController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpGet("GetSubject")]
    public async Task<IActionResult> GetSubject(Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new GetSubjectQuereis(id));
        return Ok(res);
    }
    [HttpPut("CreateSubject")]
    public async Task<IActionResult> CreateSubject(SubjectCreateModel subjectCreateModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new CreateSubjectCommand(subjectCreateModel));
        return Ok(res);
    }
    [HttpPost("UpdateSubject")]
    public async Task<IActionResult> UpdateSubject(SubjectUpdateModel subjectUpdateModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new UpdateSubjectCommand(subjectUpdateModel));
        return Ok(res);
    }
    [HttpDelete("DeleteSubject")]
    public async Task<IActionResult> DeleteSubject(Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new DeleteSubjectCommand(id));
        return Ok(res);
    }
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new DeleteSubjectCommand(id));
        return Ok(res);
    }
}
