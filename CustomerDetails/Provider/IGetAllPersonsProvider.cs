using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerDetails.Core;

namespace CustomerDetails.Provider;

public interface IGetAllPersonsProvider
{
    Task<IList<Person>> GetAsync();
}