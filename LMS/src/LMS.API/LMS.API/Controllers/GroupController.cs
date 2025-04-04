using LMS.Application.DTO.Create;
using LMS.Application.DTO.Update;
using LMS.Application.Feature.Group.Command;
using LMS.Application.Feature.Group.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers;

public class GroupController : Controller
{
   private readonly IMediator mediator;
    public GroupController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpGet("GetGroup")]
    public async Task<IActionResult> GetGroup(Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new GetGroupQueries(id));
        return Ok(res);
    }
    [HttpPut("CreateGroup")]
    public async Task<IActionResult> CreateGroup(GroupCreateModel groupCreateModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new CreateGroupCommand(groupCreateModel));
        return Ok(res);
    }
    [HttpPost("UpdateGroup")]
    public async Task<IActionResult> UpdateGroup(GroupUpdateModel groupUpdateModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new  UpdateGroupCommand(groupUpdateModel));
        return Ok(res);
    }
    [HttpDelete("DeleteGroup")]
    public async Task<IActionResult> DeleteGroup(Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new DeleteGroupCommand(id));
        return Ok(res);
    }
}
