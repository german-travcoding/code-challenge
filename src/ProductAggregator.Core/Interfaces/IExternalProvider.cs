using ProductAggregator.Core.Models;

namespace ProductAggregator.Core.Interfaces;

/// <summary>
/// Base interface for external data providers.
/// </summary>
public interface IExternalProvider
{
    string ProviderId { get; }
    string ProviderName { get; }
}

/// <summary>
/// Interface for price data providers.
/// </summary>
public interface IPriceProvider : IExternalProvider
{
    Task<PriceProviderResponse> GetPriceAsync(string productId, CancellationToken cancellationToken = default);
}

/// <summary>
/// Interface for stock/inventory providers.
/// </summary>
public interface IStockProvider : IExternalProvider
{
    Task<StockProviderResponse> GetStockAsync(string productId, CancellationToken cancellationToken = default);
}
