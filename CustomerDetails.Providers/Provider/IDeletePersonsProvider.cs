using CustomerDetails.Core;

namespace CustomerDetails.Providers.Provider;

public interface IDeletePersonsProvider
{
    Task<bool> DeleteAsync(Person person);
}