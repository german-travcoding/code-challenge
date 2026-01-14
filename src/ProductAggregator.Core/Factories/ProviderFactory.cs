using ProductAggregator.Core.Interfaces;
using ProductAggregator.Core.Services.MockProviders;

namespace ProductAggregator.Core.Factories;

/// <summary>
/// Factory for creating and managing provider instances.
/// Demonstrates the Factory pattern for creating objects.
/// 
/// Candidates should consider:
/// - How to extend this for new provider types
/// - Whether to use Abstract Factory pattern
/// - How to handle provider lifecycle (transient vs singleton)
/// </summary>
public class ProviderFactory : IProviderFactory
{
    private readonly Dictionary<string, IPriceProvider> _priceProviders;
    private readonly Dictionary<string, IStockProvider> _stockProviders;

    public ProviderFactory()
    {
        // Initialize price providers
        _priceProviders = new Dictionary<string, IPriceProvider>
        {
            { "PRICE_A", new MockPriceProviderA() },
            { "PRICE_B", new MockPriceProviderB() },
            { "PRICE_C", new MockPriceProviderC() }
        };

        // Initialize stock providers
        _stockProviders = new Dictionary<string, IStockProvider>
        {
            { "STOCK_EAST", new MockStockProviderEast() },
            { "STOCK_WEST", new MockStockProviderWest() }
        };
    }

    public IEnumerable<IPriceProvider> GetPriceProviders() => _priceProviders.Values;

    public IEnumerable<IStockProvider> GetStockProviders() => _stockProviders.Values;

    public IPriceProvider? GetPriceProvider(string providerId)
    {
        _priceProviders.TryGetValue(providerId, out var provider);
        return provider;
    }

    public IStockProvider? GetStockProvider(string providerId)
    {
        _stockProviders.TryGetValue(providerId, out var provider);
        return provider;
    }
}
