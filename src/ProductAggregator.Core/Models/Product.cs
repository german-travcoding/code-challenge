namespace ProductAggregator.Core.Models;

/// <summary>
/// Represents a product with aggregated information from multiple providers.
/// </summary>
public class Product
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public List<PriceInfo> Prices { get; set; } = new();
    public List<StockInfo> StockLevels { get; set; } = new();
}

/// <summary>
/// Price information from a specific provider.
/// </summary>
public class PriceInfo
{
    public string ProviderId { get; set; } = string.Empty;
    public string ProviderName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Currency { get; set; } = "USD";
    public DateTime LastUpdated { get; set; }
}

/// <summary>
/// Stock availability from a specific warehouse.
/// </summary>
public class StockInfo
{
    public string WarehouseId { get; set; } = string.Empty;
    public string WarehouseName { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public bool IsAvailable => Quantity > 0;
}
