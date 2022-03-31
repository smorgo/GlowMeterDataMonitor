using System.Text.Json.Serialization;
// - 0C: Alternative Historical Consumption
//   - 01: CurrentDayConsumptionDelivered
//   - 30: CurrentWeekConsumptionDelivered
//   - 40: CurrentMonthConsumptionDelivered
public class AlternativeHistoricalConsumption
{
    [JsonPropertyName("01")]
    public string? CurrentDayConsumptionDelivered { get; set; }
    [JsonPropertyName("30")]
    public string? CurrentWeekConsumptionDelivered { get; set; }
    [JsonPropertyName("40")]
    public string? CurrentMonthConsumptionDelivered { get; set; }
}
