using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
// - 00: Reading Information Set
//   - 00: CurrentSummationDelivered: meter reading
//   - 01: CurrentSummationReceived
//   - 02: CurrentMaxDemandDelivered
//   - 07: ReadingSnapshotTime (UTC time)
//   - 14: Supply Status (enum): 0x2 is on
public class Reading
{
    [JsonPropertyName("00")]
    public string? CurrentSummationDelivered { get; set; }
    [JsonPropertyName("01")]
    public string? CurrentSummationReceived { get; set; }
    [JsonPropertyName("02")]
    public string? CurrentMaxDemandDelivered { get; set; }
    [JsonPropertyName("07")]
    public string? ReadingSnapshotTime { get; set; }
    [JsonPropertyName("14")]
    public string? SupplyStatus { get; set; }
    [NotMapped]
    public bool SupplyStatusOn => SupplyStatus.FromHexToInt() == 2;
    [NotMapped]
    public DateTime? ReadingSnapshotTimeUtc => ReadingSnapshotTime.ToUtcDateTime();
}
