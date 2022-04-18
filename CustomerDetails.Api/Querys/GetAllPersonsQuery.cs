using CustomerDetails.Api.Data;
using CustomerDetails.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CustomerDetails.Api.Querys;

    public class GetAllPersonsQuery : IRequest<IEnumerable<Person>>   {/* could do some fluent valadation here but happy path.. just keep swimining */   }

    public class GetAllPersonsQueryHandler : IRequestHandler<GetAllPersonsQuery, IEnumerable<Person>>
    {
        private readonly AppDbContext context;

        public GetAllPersonsQueryHandler(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Person>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
        {
            return await context.Person.Include(p => p.Profession)
                             .ToListAsync();
        }
    }