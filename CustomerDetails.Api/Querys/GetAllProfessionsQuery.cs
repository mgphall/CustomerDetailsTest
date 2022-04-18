using CustomerDetails.Api.Data;
using CustomerDetails.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CustomerDetails.Api.Querys;

    public class GetAllProfessionsQuery : IRequest<IEnumerable<Profession>>   {/* could do some fluent valadation here but happy path.. just keep swimining */   }

    public class GetAllProfessionsQueryHandler : IRequestHandler<GetAllProfessionsQuery, IEnumerable<Profession>>
    {
        private readonly AppDbContext context;

        public GetAllProfessionsQueryHandler(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Profession>> Handle(GetAllProfessionsQuery request, CancellationToken cancellationToken)
        {
            var items = await context.Profession.ToListAsync();

            return items;
        }
    }

