using System.Threading.Tasks;
using CustomerDetails.Core;

namespace CustomerDetails.Provider;

public interface IAddPersonsProvider
{
    Task<Person> PostAsync(Person person);
}