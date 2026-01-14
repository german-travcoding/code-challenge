using ProductAggregator.Core.Interfaces;
using ProductAggregator.Core.Models;

namespace ProductAggregator.Core.Services.MockProviders;

/// <summary>
/// Simulated stock provider for East Coast warehouses.
/// </summary>
public class MockStockProviderEast : IStockProvider
{
    private static readonly Random _random = new();
    
    public string ProviderId => "STOCK_EAST";
    public string ProviderName => "East Coast Inventory";

    public async Task<StockProviderResponse> GetStockAsync(string productId, CancellationToken cancellationToken = default)
    {
        // Simulate API latency (250-600ms)
        await Task.Delay(_random.Next(250, 600), cancellationToken);

        if (_random.NextDouble() < 0.05)
        {
            return new StockProviderResponse
            {
                Success = false,
                ErrorMessage = "East Coast inventory service unavailable",
                ProductId = productId
            };
        }

        var warehouses = new[] { "NYC", "BOS", "MIA", "ATL" };
        var stockInfos = warehouses.Select(wh => new StockInfo
        {
            WarehouseId = $"WH_{wh}",
            WarehouseName = $"{wh} Distribution Center",
            Location = $"East Coast - {wh}",
            Quantity = Math.Abs((productId + wh).GetHashCode() % 100)
        }).ToList();

        return new StockProviderResponse
        {
            Success = true,
            ProductId = productId,
            StockInfos = stockInfos
        };
    }
}

/// <summary>
/// Simulated stock provider for West Coast warehouses.
/// </summary>
public class MockStockProviderWest : IStockProvider
{
    private static readonly Random _random = new();
    
    public string ProviderId => "STOCK_WEST";
    public string ProviderName => "West Coast Inventory";

    public async Task<StockProviderResponse> GetStockAsync(string productId, CancellationToken cancellationToken = default)
    {
        // Simulate API latency (200-500ms)
        await Task.Delay(_random.Next(200, 500), cancellationToken);

        var warehouses = new[] { "LAX", "SEA", "SFO", "PHX" };
        var stockInfos = warehouses.Select(wh => new StockInfo
        {
            WarehouseId = $"WH_{wh}",
            WarehouseName = $"{wh} Distribution Center",
            Location = $"West Coast - {wh}",
            Quantity = Math.Abs((productId + wh).GetHashCode() % 150)
        }).ToList();

        return new StockProviderResponse
        {
            Success = true,
            ProductId = productId,
            StockInfos = stockInfos
        };
    }
}

