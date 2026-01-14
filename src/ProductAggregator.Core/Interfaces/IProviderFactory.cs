namespace ProductAggregator.Core.Interfaces;

/// <summary>
/// Factory pattern interface for creating provider instances.
/// Candidates should implement this to demonstrate Factory pattern knowledge.
/// </summary>
public interface IProviderFactory
{
    /// <summary>
    /// Gets all registered price providers.
    /// </summary>
    IEnumerable<IPriceProvider> GetPriceProviders();

    /// <summary>
    /// Gets all registered stock providers.
    /// </summary>
    IEnumerable<IStockProvider> GetStockProviders();

    /// <summary>
    /// Gets a specific price provider by ID.
    /// </summary>
    /// <param name="providerId">Provider identifier.</param>
    IPriceProvider? GetPriceProvider(string providerId);

    /// <summary>
    /// Gets a specific stock provider by ID.
    /// </summary>
    /// <param name="providerId">Provider identifier.</param>
    IStockProvider? GetStockProvider(string providerId);
}
