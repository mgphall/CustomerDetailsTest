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

namespace CustomerDetails.Tests
{
    public class UpdatePersonsQueryHandlerTests
    {
        public UpdatePersonsQueryHandlerTests()
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
        public async Task UpdatedPersonsQueryHandler_UpdatePerson_SuccessAsync()
        {
            Context.Database.EnsureDeletedAsync();
            var handler = new UpdatePersonsQueryHandler(Context);

            var pro = new Profession() { Id = Guid.NewGuid(), Title = "The boss" };

            var id = Guid.NewGuid();
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

            var updatedPerson = new Person()
            {
                Id = id,
                DateOfBirth = DateTime.MinValue,
                FirstName = "Pei",
                LastName = "Ang",
                Profession = pro,
                ProfessionId = pro.Id
            };

            var query = new UpdatePersonsQuery(updatedPerson);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Same(updatedPerson.Profession, person.Profession);
            Assert.Equal(updatedPerson.FirstName, person.FirstName);
            Assert.Equal(updatedPerson.LastName, person.LastName);
            Assert.Equal(updatedPerson.DateOfBirth, person.DateOfBirth);

            var count = Context.Person.Count();
            Assert.Equal(1, count);
        }
    }
}