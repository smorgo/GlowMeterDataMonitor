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
// - 03: Formatting
//   - 00: UnitofMeasure (enum): 00 means kWh, 01 means m3
//   - 01: Multiplier
//   - 02: Divisor
//   - 03: SummationFormatting (bit map):
//         2B means 3 digits after the decimal point, 2 digits before the decimal point
//         FB means 3 digits after the decimal point, 16 digits before the decimal point,
//         no leading zeros
//   - 04: DemandFormatting
//   - 06: MeteringDeviceType: 00 means Electric Metering, 80 means Mirrored Gas Metering
//   - 07: SiteID: MPAN encoded in UTF-8
//   - 08: MeterSerialNumber (string)
//   - 12: AlternativeUnitofMeasure (enum)
public class Formatting
{
    [JsonPropertyName("00")]
    public string? UnitofMeasure { get; set; }
    [JsonPropertyName("01")]
    public string? Multiplier { get; set; }
    [JsonPropertyName("02")]
    public string? Divisor { get; set; }
    [JsonPropertyName("03")]
    public string? SummationFormatting { get; set; }
    [JsonPropertyName("04")]
    public string? DemandFormatting { get; set; }
    [JsonPropertyName("06")]
    public string? MeteringDeviceType { get; set; }
    [JsonPropertyName("07")]
    public string? SiteID { get; set; }
    [JsonPropertyName("08")]
    public string? MeterSerialNumber { get; set; }
    [JsonPropertyName("12")]
    public string? AlternativeUnitofMeasure { get; set; }
    [NotMapped]
    public bool IsKwh => UnitofMeasure.FromHexToInt() == 0;
    [NotMapped]
    public bool IsM3 => UnitofMeasure.FromHexToInt() == 1;
    [NotMapped]
    public int DigitsBeforeDp => 3;
    [NotMapped]
    public int DigitsAfterDp => SummationFormatting.FromHexToInt() == 0x2B ? 2 : 16;
    [NotMapped]
    public bool IsElectricMetering => MeteringDeviceType.FromHexToInt() == 0;
    [NotMapped]
    public bool IsMirroredGasMetering => MeteringDeviceType.FromHexToInt() == 0x80;
    [NotMapped]
    public decimal MultiplierValue => (decimal)Multiplier.FromHexToInt();
    [NotMapped]
    public decimal DivisorValue => (decimal)Divisor.FromHexToInt();
    [NotMapped]
    public string UnitsLabel => IsKwh ? "kWh" : "m3";
}
