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
    public class GetAllProfessionQueryTests
    {
        public GetAllProfessionQueryTests()
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
            var handler = new GetAllProfessionsQueryHandler(Context);

            var pro = new Profession() { Id = Guid.NewGuid(), Title = "The boss" };
            var pro1 = new Profession() { Id = Guid.NewGuid(), Title = "Not The boss" };

            await Context.Profession.AddRangeAsync( new List<Profession>{ pro, pro1});
    
            await Context.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);

            var query = new GetAllProfessionsQuery();

            var result = await handler.Handle(query, CancellationToken.None);
 
            var professionCount = Context.Profession.Count();
            Assert.Equal(2, professionCount);
        }
    }
}