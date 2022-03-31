using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
// - 02: Meter Status
//   - 00: Status (bit map): 10 means power quality event
public class MeterStatus
{
    [JsonPropertyName("00")]
    public string? Status { get; set; } 
    [NotMapped]
    public bool PowerQualityEvent => Status.FromHexToInt() == 0x10;
}
