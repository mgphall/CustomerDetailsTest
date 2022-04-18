using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CustomerDetails.Api.Data;
using CustomerDetails.Api.Querys;
using CustomerDetails.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Xunit;

namespace CustomerDetails.Tests.HandlerTests
{
    public class AddPersonsQueryHandlerTests
    {
        public AddPersonsQueryHandlerTests()
        {
            var _contextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("testDB" + DateTime.Now.Ticks)
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            
            
              Context = new AppDbContext(_contextOptions);

              Context.Database.EnsureDeleted();
              Context.Database.EnsureCreated();

        }

        
        public AppDbContext Context { get; set; }

        [Fact]
        public async Task AddPersonsQueryHandler_DeletePerson_SuccessAsync()
        {
            Context.Database.EnsureDeletedAsync();
            var handler = new AddPersonsQueryHandler(Context);

            var pro = new Profession() { Id = Guid.NewGuid(), Title = "The boss" };
            await Context.Profession.AddAsync(pro);
            await Context.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);


            var person = new Person()
            {
                DateOfBirth = DateTime.Today, FirstName = "Matthew", LastName = "Hall", Profession = pro,
                ProfessionId = pro.Id
            };

            var query = new AddPersonsQuery(person);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Same(result.Profession, person.Profession);
            Assert.Equal(result.FirstName, person.FirstName);
            Assert.Equal(result.LastName, person.LastName);
            Assert.Equal(result.DateOfBirth, person.DateOfBirth);

            var count = Context.Profession.Count();
            Assert.Equal(1, count);
        }
    }
}