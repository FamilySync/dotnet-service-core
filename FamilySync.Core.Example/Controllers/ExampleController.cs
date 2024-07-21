using FamilySync.Core.Example.Models.DTOs;
using FamilySync.Core.Example.Models.Requests;
using FamilySync.Core.Example.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilySync.Core.Example.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ExampleController : ControllerBase
{
    private readonly IExampleService _service;

    public ExampleController(IExampleService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ExampleDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExampleDTO>> Create([FromBody] CreateExampleRequest request, CancellationToken cancellationToken)
    {
        var result = await _service.Create(request, cancellationToken);

        return CreatedAtAction(nameof(Get), new {result.ID}, result);
    }

    [Authorize("familysync:user")]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExampleDTO>> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var response = await _service.Get(id, cancellationToken);

        return Ok(response);
    }

    [Authorize("familysync:Admin")]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _service.Delete(id, cancellationToken);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExampleDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExampleDTO>> Update([FromRoute] Guid id, [FromBody] UpdateExampleRequest request, CancellationToken cancellationToken)
    {
        var result = await _service.Update(id, request, cancellationToken);

        return Ok(result);
    }
}