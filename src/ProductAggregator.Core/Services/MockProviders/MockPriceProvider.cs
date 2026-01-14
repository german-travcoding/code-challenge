using ProductAggregator.Core.Interfaces;
using ProductAggregator.Core.Models;

namespace ProductAggregator.Core.Services.MockProviders;

/// <summary>
/// Simulated price provider A - Simulates an external API with latency.
/// </summary>
public class MockPriceProviderA : IPriceProvider
{
    private static readonly Random _random = new();
    
    public string ProviderId => "PRICE_A";
    public string ProviderName => "PriceWatch API";

    public async Task<PriceProviderResponse> GetPriceAsync(string productId, CancellationToken cancellationToken = default)
    {
        // Simulate API latency (200-500ms)
        await Task.Delay(_random.Next(200, 500), cancellationToken);

        // Simulate occasional failures (5% chance)
        if (_random.NextDouble() < 0.05)
        {
            return new PriceProviderResponse
            {
                Success = false,
                ErrorMessage = "Provider temporarily unavailable",
                ProductId = productId
            };
        }

        // Generate deterministic but varied prices based on product ID
        var basePrice = Math.Abs(productId.GetHashCode() % 1000) + 10.0m;
        
        return new PriceProviderResponse
        {
            Success = true,
            ProductId = productId,
            PriceInfo = new PriceInfo
            {
                ProviderId = ProviderId,
                ProviderName = ProviderName,
                Price = basePrice + _random.Next(0, 50) / 10.0m,
                Currency = "USD",
                LastUpdated = DateTime.UtcNow
            }
        };
    }
}

/// <summary>
/// Simulated price provider B - Different pricing strategy.
/// </summary>
public class MockPriceProviderB : IPriceProvider
{
    private static readonly Random _random = new();
    
    public string ProviderId => "PRICE_B";
    public string ProviderName => "BestPrice API";

    public async Task<PriceProviderResponse> GetPriceAsync(string productId, CancellationToken cancellationToken = default)
    {
        // Simulate API latency (300-700ms) - Slower provider
        await Task.Delay(_random.Next(300, 700), cancellationToken);

        var basePrice = Math.Abs(productId.GetHashCode() % 1000) + 5.0m;
        
        return new PriceProviderResponse
        {
            Success = true,
            ProductId = productId,
            PriceInfo = new PriceInfo
            {
                ProviderId = ProviderId,
                ProviderName = ProviderName,
                Price = basePrice + _random.Next(0, 30) / 10.0m,
                Currency = "USD",
                LastUpdated = DateTime.UtcNow
            }
        };
    }
}

/// <summary>
/// Simulated price provider C - Premium provider with best prices.
/// </summary>
public class MockPriceProviderC : IPriceProvider
{
    private static readonly Random _random = new();
    
    public string ProviderId => "PRICE_C";
    public string ProviderName => "PremiumDeals API";

    public async Task<PriceProviderResponse> GetPriceAsync(string productId, CancellationToken cancellationToken = default)
    {
        // Simulate API latency (150-350ms) - Faster provider
        await Task.Delay(_random.Next(150, 350), cancellationToken);

        var basePrice = Math.Abs(productId.GetHashCode() % 1000) * 0.9m;
        
        return new PriceProviderResponse
        {
            Success = true,
            ProductId = productId,
            PriceInfo = new PriceInfo
            {
                ProviderId = ProviderId,
                ProviderName = ProviderName,
                Price = basePrice + _random.Next(0, 20) / 10.0m,
                Currency = "USD",
                LastUpdated = DateTime.UtcNow
            }
        };
    }
}


