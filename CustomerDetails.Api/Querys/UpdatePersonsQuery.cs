using CustomerDetails.Api.Data;
using CustomerDetails.Core;
using MediatR;

namespace CustomerDetails.Api.Querys
{
    public class UpdatePersonsQuery : IRequest<bool>
    {
        public Person Person { get; set; }

        public UpdatePersonsQuery(Person person)
        {
            Person = person;
        }
    }

    public class UpdatePersonsQueryHandler : IRequestHandler<UpdatePersonsQuery, bool>
    {
        private readonly AppDbContext _context;

        public UpdatePersonsQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdatePersonsQuery request, CancellationToken cancellationToken)
        {
            var person = _context.Person.FirstOrDefault<Person>(p => p.Id == request.Person.Id);

            if (person != null) 
            {
                var details = request.Person;

                person.FirstName = details.FirstName;
                person.LastName = details.LastName;
                person.DateOfBirth = details.DateOfBirth;
                person.ProfessionId = details.ProfessionId;

                var modifiedRows = await _context.SaveChangesAsync();

                return modifiedRows > 0;
            }

            return false;
        }
    }
}
