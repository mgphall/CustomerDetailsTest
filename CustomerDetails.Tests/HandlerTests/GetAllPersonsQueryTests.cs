using System;
using System.Collections.Generic;
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
    public class GetAllPersonsQueryTests
    {
        public GetAllPersonsQueryTests()
        {
            var _contextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("testDB")
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
            var handler = new GetAllPersonsQueryHandler(Context);

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

            var person2 = new Person()
            {
                Id = Guid.NewGuid(),
                DateOfBirth = DateTime.Today,
                FirstName = "Pei",
                LastName = "Ang",
                Profession = pro,
                ProfessionId = pro.Id
            };

            await Context.Profession.AddAsync(pro);
            await Context.Person.AddRangeAsync(new List<Person>{ person , person2 });
            await Context.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);

 

            var query = new GetAllPersonsQuery();

            var result = await handler.Handle(query, CancellationToken.None);

            var count = Context.Person.Count();
            Assert.Equal(2, count);

            var professionCount = Context.Profession.Count();
            Assert.Equal(1, professionCount);
        }
    }
}