using System.Text.Json;

public class OutgoingMeteringMessage
{
    public DateTime Timestamp { get; set; }
    public decimal ElectricityInstant { get; set; }
    public decimal ElectricityDaily { get; set; }
    public decimal ElectricityWeekly { get; set; }
    public decimal ElectricityMonthly { get; set; }
    public string? ElectricityUnits { get; set; }
    public decimal GasInstant { get; set; }
    public decimal GasDaily { get; set; }
    public decimal GasWeekly { get; set; }
    public decimal GasMonthly { get; set; }
    public string? GasUnits { get; set; }

    public static OutgoingMeteringMessage? FromGlowMqttMessage(GlowMqttMessage? message)
    {
        if(message == null) return null;

        return new OutgoingMeteringMessage
        {
            Timestamp = message.TimestampUtc,
            ElectricityInstant = message?.Electricity?.Metering?.InstantaneousDemand ?? 0,
            ElectricityDaily = message?.Electricity?.Metering?.DailyConsumption ?? 0,
            ElectricityWeekly = message?.Electricity?.Metering?.WeeklyConsumption ?? 0,
            ElectricityMonthly = message?.Electricity?.Metering?.MonthlyConsumption ?? 0,
            ElectricityUnits = message?.Electricity?.Metering?.Formatting?.UnitsLabel ?? string.Empty,
            GasInstant = message?.Gas?.Metering?.InstantaneousDemand ?? 0,
            GasDaily = message?.Gas?.Metering?.DailyConsumption ?? 0,
            GasWeekly = message?.Gas?.Metering?.WeeklyConsumption ?? 0,
            GasMonthly = message?.Gas?.Metering?.MonthlyConsumption ?? 0,
            GasUnits = message?.Gas?.Metering?.Formatting?.UnitsLabel ?? string.Empty
        };
    }

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }
}