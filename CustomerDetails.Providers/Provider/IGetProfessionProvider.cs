using CustomerDetails.Core;

namespace CustomerDetails.Providers.Provider;

public interface IGetProfessionProvider
{
    Task<List<Profession>> GetProductAsync();
}