namespace ProductAggregator.Core.Models;

/// <summary>
/// Base response from external providers.
/// </summary>
public abstract class ProviderResponse
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Response from price provider APIs.
/// </summary>
public class PriceProviderResponse : ProviderResponse
{
    public string ProductId { get; set; } = string.Empty;
    public PriceInfo? PriceInfo { get; set; }
}

/// <summary>
/// Response from stock/inventory provider APIs.
/// </summary>
public class StockProviderResponse : ProviderResponse
{
    public string ProductId { get; set; } = string.Empty;
    public List<StockInfo> StockInfos { get; set; } = new();
}
