using System.Text.Json.Serialization;
// - 04: Historical Consumption
//   - 00: InstantaneousDemand (signed): current consumption
//   - 01: CurrentDayConsumptionDelivered
//   - 30: CurrentWeekConsumptionDelivered
//   - 40: CurrentMonthConsumptionDelivered
public class HistoricalConsumption
{
    [JsonPropertyName("00")]
    public string? InstantaneousDemand { get; set; }
    [JsonPropertyName("01")]
    public string? CurrentDayConsumptionDelivered { get; set; }
    [JsonPropertyName("30")]
    public string? CurrentWeekConsumptionDelivered { get; set; }
    [JsonPropertyName("40")]
    public string? CurrentMonthConsumptionDelivered { get; set; }
}
