using CustomerDetails.Api.Querys;
using CustomerDetails.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerDetails.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonController : Controller
{
    private readonly IMediator mediator;
    private readonly ILogger<PersonController> _logger;

    public PersonController(IMediator mediator, ILogger<PersonController> logger)
    {
        this.mediator = mediator;
        _logger = logger;
    }

    [HttpGet(Name = "GetAllPerson")]
    public async Task<IActionResult> GetAsync()
    {
        GetAllPersonsQuery getAllProfessionsQuery = new GetAllPersonsQuery();

        var result = await mediator.Send(getAllProfessionsQuery);

        return Ok(result);
    }

    [HttpDelete(Name = "DeletePerson")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var deletePersonsQuery = new DeletePersonsQuery(id);

        var result = await mediator.Send(deletePersonsQuery);

        return Ok(result);
    }

    [HttpPost(Name = "AddPerson")]
    public async Task<IActionResult> AddAsync(Person person)
    {
        var addPersonsQuery = new AddPersonsQuery(person);

        var result = await mediator.Send(addPersonsQuery);
        return Ok(result);
    }

    [HttpPut(Name = "Person")]
    public async Task<IActionResult> UpdateAsync(Person person)
    {
        var addPersonsQuery = new UpdatePersonsQuery(person);

        var result = await mediator.Send(addPersonsQuery);

        return result ? Ok(result) : NotFound(result);
    }
}