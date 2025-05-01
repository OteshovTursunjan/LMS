using LMS.Application.DTO.Create;
using LMS.Application.DTO.Update;
using LMS.Application.Feature.TuitionFee.Command;
using LMS.Application.Feature.TuitionFee.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers;
[Authorize(Policy = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class TuitionFeeController : Controller
{
    private readonly IMediator mediator;
    public TuitionFeeController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpGet("GetTuitionFee")]
    public async Task<IActionResult> Get(Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new GetTuitionQueries(id));   
        return Ok(res);
    }
    [HttpPut("CreateTuitionFee")]
    public async Task<IActionResult> CreateTuitionFee(TuitionFeeModel tuitionFeeModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new CreateTuitioFeeCommand(tuitionFeeModel));
        return Ok(res);
    }
    [HttpPost("UpdateTuitionFee")]
    public async Task<IActionResult> UpdateTuitionFee(TuitionFeeUpdateModel tuitionFeeUpdateModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new UpdateTuitionFeeCommand(tuitionFeeUpdateModel));
        return Ok(res);
    }
    [HttpDelete("DeleteTuitionFee")]
    public async Task<IActionResult> DeleteTuitionFee(Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new DeleteTuitionFeeCommand(id));
        return Ok(res);
    }
}
