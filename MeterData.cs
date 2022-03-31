
using System.Text.Json.Serialization;

public class MeterData
{
    [JsonPropertyName("0702")]
    public Metering? Metering { get; set; }
    [JsonPropertyName("0705")]
    public Prepayment? Prepayment { get; set; }
    [JsonPropertyName("0708")]
    public DeviceManagement? DeviceManagement { get; set; }
}
