using LMS.Application.DTO.Create;
using LMS.Application.Feature.Faculty.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers;
[Authorize] 
[ApiController]
[Route("api/[controller]")]
public class FacultyController : Controller
    
{
    private readonly IMediator mediator;
    public FacultyController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpPut("CreateFaculty")]
    public async Task<IActionResult> CreateFaculty(FacultyCreateModel facultyCreateModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new CreateFacultyCommand(facultyCreateModel));
        return Ok(res);
    }
  
}
