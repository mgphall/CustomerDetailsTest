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
    public class PersonControllerTests
    {
        private readonly Mock<IMediator> _mediator;

        public PersonControllerTests()
        {
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public async Task PersonController_GetAllPersonsQuery_ReturnsOk()
        {
            var test = new PersonController(_mediator.Object, null);
            var pro = new Profession() { Id = Guid.NewGuid(), Title = "The boss" };

            var person = new Person()
            {
                Id = Guid.NewGuid(),
                DateOfBirth = DateTime.Today,
                FirstName = "Matthew",
                LastName = "Hall",
                Profession = pro,
                ProfessionId = pro.Id
            };
            var items = new List<Person>{ person };

            _mediator.Setup(x => x.Send(It.IsAny<GetAllPersonsQuery>(), CancellationToken.None)).ReturnsAsync(items);

            var result =test.GetAsync();

            Assert.NotNull(result);
            var okResult = result.Result;
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task PersonController_DeletePersonsQuery_ReturnsOk()
        {
            var test = new PersonController(_mediator.Object, null);
            
            _mediator.Setup(x => x.Send(It.IsAny<DeletePersonsQuery>(), CancellationToken.None)).ReturnsAsync(true);

            var result = test.DeleteAsync(It.IsAny<Guid>());

            Assert.NotNull(result);
            var okResult = result.Result;
            Assert.IsType<OkObjectResult>(okResult);
        }
 
        [Fact]
        public async Task PersonController_AddPersonsQuery_ReturnsOk()
        {
            var test = new PersonController(_mediator.Object, null);

            _mediator.Setup(x => x.Send(It.IsAny<AddPersonsQuery>(), CancellationToken.None)).ReturnsAsync(It.IsAny<Person>());

            var result = test.AddAsync(It.IsAny<Person>());

            Assert.NotNull(result);
            var notFound = result.Result;
            Assert.IsType<OkObjectResult>(notFound);
        }

        [Fact]
        public async Task PersonController_UpdatePersonsQuery_ReturnsOk()
        {
            var test = new PersonController(_mediator.Object, null);

            _mediator.Setup(x => x.Send(It.IsAny<UpdatePersonsQuery>(), CancellationToken.None)).ReturnsAsync(true);

            var result = test.UpdateAsync(It.IsAny<Person>());

            Assert.NotNull(result);
            var okResult = result.Result;
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task PersonController_UpdatePersonsQuery_ReturnNotFound()
        {
            var test = new PersonController(_mediator.Object, null);

            _mediator.Setup(x => x.Send(It.IsAny<UpdatePersonsQuery>(), CancellationToken.None)).ReturnsAsync(false);

            var result = test.UpdateAsync(It.IsAny<Person>());

            Assert.NotNull(result);
            var okResult = result.Result;
            Assert.IsType<NotFoundObjectResult>(okResult);
        }
    }
}
