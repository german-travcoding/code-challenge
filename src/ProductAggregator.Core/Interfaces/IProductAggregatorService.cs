using ProductAggregator.Core.Models;

namespace ProductAggregator.Core.Interfaces;

/// <summary>
/// Service for aggregating product information from multiple sources.
/// </summary>
public interface IProductAggregatorService
{
    /// <summary>
    /// Aggregates product information from multiple external providers.
    /// </summary>
    /// <param name="request">The aggregation request containing product IDs and options.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Aggregated product response.</returns>
    Task<AggregatedProductResponse> AggregateProductsAsync(
        AggregatedProductRequest request, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a single aggregated product by ID.
    /// </summary>
    /// <param name="productId">The product identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The aggregated product or null if not found.</returns>
    Task<Product?> GetProductAsync(string productId, CancellationToken cancellationToken = default);
}

