using System.Threading.Tasks;
using CustomerDetails.Core;

namespace CustomerDetails.Provider;

public interface IUpdatePersonsProvider
{
    Task<bool> PostAsync(Person person);
}