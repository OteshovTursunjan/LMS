using LMS.Application.DTO.Create;
using LMS.Application.DTO.Update;
using LMS.Application.Feature.Faculty.Command;
using LMS.Application.Feature.Faculty.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers;
[Authorize(Policy = "Admin")] 
[ApiController]
[Route("api/[controller]")]
public class FacultyController : Controller
    
{
    private readonly IMediator mediator;
    public FacultyController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpGet("GetFaculty")]
    public async Task<IActionResult> GetFaculty(Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new GetFacultyQueries(id));
        return Ok(res);

    }
    [HttpPut("CreateFaculty")]
    public async Task<IActionResult> CreateFaculty(FacultyCreateModel facultyCreateModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new CreateFacultyCommand(facultyCreateModel));
        return Ok(res);
    }
    [HttpPost("UpdateFaculty")]
    public async Task<IActionResult> UpdateFaculty(FacultyUpdateModel facultyUpdateModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new UpdateFacultyCommand(facultyUpdateModel));
        return Ok(res);
    }
    [HttpDelete("DeletFaculty")]
    public async Task<IActionResult> DeleteFaculty(Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new DeleteFacultyCommand(id));    
        return Ok(res);
    }
  
}
