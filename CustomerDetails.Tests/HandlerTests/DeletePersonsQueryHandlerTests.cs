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
    public class DeletePersonsQueryHandlerTests
    {
        public DeletePersonsQueryHandlerTests()
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
        public async Task DeletePersonsQueryHandler_RemovePerson_SuccessAsync()
        {
            Context.Database.EnsureDeletedAsync();
            var handler = new DeletePersonsQueryHandler(Context);
            var id = Guid.NewGuid();
            var pro = new Profession() { Id = Guid.NewGuid(), Title = "The boss" };
            var person = new Person()
            {
                Id = id,
                DateOfBirth = DateTime.Today,
                FirstName = "Matthew",
                LastName = "Hall",
                Profession = pro,
                ProfessionId = pro.Id
            };
            await Context.Profession.AddAsync(pro);
            await Context.Person.AddAsync(person);

            await Context.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);

            var query = new DeletePersonsQuery(id);

            var result = await handler.Handle(query, CancellationToken.None);
            var professionCount = Context.Profession.Count();

            var count = Context.Person.Count();
            Assert.Equal(0, count);
            Assert.Equal(1, professionCount);
        }
    }
}