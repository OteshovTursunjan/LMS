﻿using LMS.Application.DTO.Create;
using LMS.Application.Feature.Contract.Command;
using LMS.Application.Feature.Contract.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers;

public class ContractController : Controller
{
    private readonly IMediator mediator;
    public ContractController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpGet("GetContract")]
    public async Task<IActionResult> GetContract(Guid id)
    {

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new GetContractQueries(id));
        return Ok(res);
    }
    [HttpPut("CreateContract")]
    public async Task<IActionResult> CreateContract(ContractCreateModel contractCreateModel)
    {

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res = await mediator.Send(new CreateContractCommand(contractCreateModel));
        return Ok(res);
    }
    [HttpDelete("DeleteContract")]
    public async Task<IActionResult> DeleteContract(Guid id)
    {

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var res  = await    mediator.Send(new DeleteContractCommand(id));
        return Ok(res);
    }
}
