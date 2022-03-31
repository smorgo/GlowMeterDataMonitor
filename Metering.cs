using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Metering 
{
    [JsonPropertyName("00")]
    public Reading? Reading {get; set;}
    [JsonPropertyName("02")]
    public MeterStatus? MeterStatus { get; set; }
    [JsonPropertyName("03")]
    public Formatting? Formatting { get; set; }
    [JsonPropertyName("04")]
    public HistoricalConsumption? HistoricalConsumption { get; set; }
    [JsonPropertyName("0C")]
    public AlternativeHistoricalConsumption? AlternativeHistoricalConsumption { get; set; }
    [NotMapped]
    public decimal InstantaneousDemand => 
        ScaleFromString(HistoricalConsumption?.InstantaneousDemand);
    [NotMapped]
    public decimal DailyConsumption => 
        ScaleFromString(HistoricalConsumption?.CurrentDayConsumptionDelivered,
            AlternativeHistoricalConsumption?.CurrentDayConsumptionDelivered);
    [NotMapped]
    public decimal WeeklyConsumption => 
        ScaleFromString(HistoricalConsumption?.CurrentWeekConsumptionDelivered,
            AlternativeHistoricalConsumption?.CurrentWeekConsumptionDelivered);
    [NotMapped]
    public decimal MonthlyConsumption => 
        ScaleFromString(HistoricalConsumption?.CurrentMonthConsumptionDelivered,
            AlternativeHistoricalConsumption?.CurrentMonthConsumptionDelivered);

    private decimal ScaleFromString(string? electricityValue, string? gasValue = null)
    {
        string? value = null;

        if(Formatting?.IsElectricMetering ?? false)
        {
            value = electricityValue;
        }
        else if(Formatting?.IsMirroredGasMetering ?? false)
        {
            value = gasValue;
        }

        if(value == null) return 0;

        var multiplier = Formatting?.MultiplierValue ?? 1m;
        var divisor = Formatting?.DivisorValue ?? 1m;

        return (decimal)(value.FromHexToLong()) * multiplier / divisor;
    }
}
