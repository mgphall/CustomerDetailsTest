using CustomerDetails.Api.Querys;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerDetails.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<ProfessionController> _logger;

        public ProfessionController(IMediator mediator, ILogger<ProfessionController> logger)
        {
            this.mediator = mediator;
            _logger = logger;
        }

        [HttpGet(Name = "GetProfession")]
        public async Task<IActionResult> GetAsync()
        {
           GetAllProfessionsQuery getAllProfessionsQuery = new GetAllProfessionsQuery();

           var result = await mediator.Send(getAllProfessionsQuery);

           return Ok(result);
        }
    }
}
