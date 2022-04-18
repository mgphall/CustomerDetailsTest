using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CustomerDetails.Api.Controllers;
using CustomerDetails.Api.Querys;
using CustomerDetails.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CustomerDetails.Tests.ControllerTests
{
    public class ProfessionControllerTests
    {
        private readonly Mock<IMediator> _mediator;

        public ProfessionControllerTests()
        {
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public async Task ProfessionController_ReturnsOk()
        {
            var test = new  ProfessionController(_mediator.Object, null);
            var pro = new Profession() { Id = Guid.NewGuid(), Title = "The boss" };
           var items = new List<Profession>{pro};

            _mediator.Setup(x => x.Send(It.IsAny<GetAllProfessionsQuery>(), CancellationToken.None)).ReturnsAsync(items);

            var result =test.GetAsync();

            Assert.NotNull(result);
            var okResult = result.Result;
            Assert.IsType<OkObjectResult>(okResult);
        }

    }
}
