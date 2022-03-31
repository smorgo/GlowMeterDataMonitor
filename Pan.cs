using System.Text.Json.Serialization;

public class Pan
{
    [JsonPropertyName("rssi")]
    public string? Rssi { get; set; }
    [JsonPropertyName("status")]
    public string? Status { get; set; }
    [JsonPropertyName("nPAN")]
    public string? Npan { get; set; }
    [JsonPropertyName("join")]
    public string? Join { get; set; }
    [JsonPropertyName("lqi")]
    public string? Lqi { get; set; }
}
