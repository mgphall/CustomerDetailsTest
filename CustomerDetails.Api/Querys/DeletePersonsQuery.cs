using CustomerDetails.Api.Data;
using MediatR;

namespace CustomerDetails.Api.Querys
{
    public class DeletePersonsQuery : IRequest<bool>
    {
        public Guid PersonId { get; set; }

        public DeletePersonsQuery(Guid personId)
        {
            PersonId = personId;
        }
    }

    public class DeletePersonsQueryHandler : IRequestHandler<DeletePersonsQuery, bool>
    {
        private readonly AppDbContext _context;

        public DeletePersonsQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeletePersonsQuery request, CancellationToken cancellationToken)
        {
            var person = _context.Person.FirstOrDefault(p => p.Id == request.PersonId);

            if (person != null) 
            {
                var result = _context.Person.Remove(person);

                var modifiedRows = await _context.SaveChangesAsync();

                return modifiedRows > 0;
            }

            return false;

        }
    }
}
