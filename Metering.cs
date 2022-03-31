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
