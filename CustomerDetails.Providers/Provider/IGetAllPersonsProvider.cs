using CustomerDetails.Core;

namespace CustomerDetails.Providers.Provider;

public interface IGetAllPersonsProvider
{
    Task<IList<Person>> GetAsync();
}