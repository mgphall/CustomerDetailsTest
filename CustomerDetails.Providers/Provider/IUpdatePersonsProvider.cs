using CustomerDetails.Core;

namespace CustomerDetails.Providers.Provider;

public interface IUpdatePersonsProvider
{
    Task<bool> PostAsync(Person person);
}