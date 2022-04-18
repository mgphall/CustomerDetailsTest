using CustomerDetails.Core;

namespace CustomerDetails.Providers.Provider;

public interface IAddPersonsProvider
{
    Task<Person> PostAsync(Person person);
}