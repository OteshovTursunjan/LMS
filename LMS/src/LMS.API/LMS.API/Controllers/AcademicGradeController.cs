using LMS.Application.DTO.Create;
using LMS.Application.Feature.AcademicGrade.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers;

public class AcademicGradeController : Controller
{

    private readonly IMediator mediator;
    public AcademicGradeController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpPost("CreateGrade")]
    public async Task<IActionResult > CreateGrade(AcademicGradeCreateModel academicGradeCreateModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new CreateAcademicGradeCommand(academicGradeCreateModel));
        return Ok(res);
    }
}
