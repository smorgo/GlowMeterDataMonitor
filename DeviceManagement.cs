using System.Text.Json.Serialization;
// 0708: Device Management
// - 01: Supplier Control Attribute Set
//   - 01: ProviderName (string)
public class DeviceManagement
{
    [JsonPropertyName("01")]
    public SupplierControlAttributeSet? SupplierControlAttributeSet { get; set; }   
}
