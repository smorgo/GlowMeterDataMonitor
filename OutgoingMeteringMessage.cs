/*
    MIT License

    Copyright (c) 2022 Steve Morgan

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*/

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