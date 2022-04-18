using CustomerDetails.Api.Data;
using CustomerDetails.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CustomerDetails.Api.Querys
{
    public class AddPersonsQuery : IRequest<Person>
    {
        public Person Person { get; set; }

        public AddPersonsQuery(Person person)
        {
            Person = person;
        }
    }

    public class AddPersonsQueryHandler : IRequestHandler<AddPersonsQuery, Person>
    {
        private readonly AppDbContext _context;

        public AddPersonsQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Person> Handle(AddPersonsQuery request, CancellationToken cancellationToken)
        {

            request.Person.Id = Guid.NewGuid();
            request.Person.Profession = null;
            var result = await _context.Person.AddAsync(request.Person, cancellationToken);

            var modifiedRows = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);


            return await _context.Person.Include(p => p.Profession).SingleAsync(x => x.Id == request.Person.Id);

        }
    }
}