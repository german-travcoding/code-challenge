using System.Diagnostics;
using ProductAggregator.Core.Interfaces;
using ProductAggregator.Core.Models;

namespace ProductAggregator.Core.Services;

public class ProductAggregatorService : IProductAggregatorService
{
    private readonly IEnumerable<IPriceProvider> _priceProviders;
    private readonly IEnumerable<IStockProvider> _stockProviders;

    public ProductAggregatorService(
        IEnumerable<IPriceProvider> priceProviders,
        IEnumerable<IStockProvider> stockProviders)
    {
        _priceProviders = priceProviders;
        _stockProviders = stockProviders;
    }

    public async Task<AggregatedProductResponse> AggregateProductsAsync(
        AggregatedProductRequest request,
        CancellationToken cancellationToken = default)
    {
        var stopwatch = Stopwatch.StartNew();
        var response = new AggregatedProductResponse
        {
            TotalRequested = request.ProductIds.Count
        };

        foreach (var productId in request.ProductIds)
        {
            try
            {
                var product = await GetProductInternalAsync(productId, request);
                if (product != null)
                {
                    response.Products.Add(product);
                    response.TotalSuccessful++;
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add($"Failed to process product {productId}: {ex.Message}");
            }
        }

        stopwatch.Stop();
        response.ProcessingTimeMs = stopwatch.ElapsedMilliseconds;
        
        return response;
    }

    public async Task<Product?> GetProductAsync(string productId, CancellationToken cancellationToken = default)
    {
        var request = new AggregatedProductRequest
        {
            ProductIds = new List<string> { productId }
        };
        
        return await GetProductInternalAsync(productId, request);
    }

    private async Task<Product?> GetProductInternalAsync(string productId, AggregatedProductRequest request)
    {
        var product = new Product
        {
            Id = productId,
            Name = $"Product {productId}",
            Description = $"Description for product {productId}",
            Category = GetCategoryFromId(productId)
        };

        if (request.IncludePrices)
        {
            foreach (var provider in _priceProviders)
            {
                try
                {
                    var priceResponse = await provider.GetPriceAsync(productId);
                    if (priceResponse.Success && priceResponse.PriceInfo != null)
                    {
                        product.Prices.Add(priceResponse.PriceInfo);
                    }
                }
                catch
                {
                    // Provider failed, continue with next
                }
            }
        }

        if (request.IncludeStock)
        {
            foreach (var provider in _stockProviders)
            {
                try
                {
                    var stockResponse = await provider.GetStockAsync(productId);
                    if (stockResponse.Success)
                    {
                        product.StockLevels.AddRange(stockResponse.StockInfos);
                    }
                }
                catch
                {
                    // Provider failed, continue with next
                }
            }
        }

        return product;
    }

    private static string GetCategoryFromId(string productId)
    {
        var hash = Math.Abs(productId.GetHashCode());
        var categories = new[] { "Electronics", "Clothing", "Home", "Sports", "Books", "Toys" };
        return categories[hash % categories.Length];
    }
}
