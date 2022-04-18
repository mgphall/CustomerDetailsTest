using System.Threading.Tasks;
using CustomerDetails.Core;

namespace CustomerDetails.Provider;

public interface IDeletePersonsProvider
{
    Task<bool> DeleteAsync(Person person);
}