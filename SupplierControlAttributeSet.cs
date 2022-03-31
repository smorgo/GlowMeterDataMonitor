using System.Text.Json.Serialization;
// - 01: Supplier Control Attribute Set
//   - 01: ProviderName (string)
public class SupplierControlAttributeSet
{
    [JsonPropertyName("01")]
    public string? ProviderName { get; set; }
}
