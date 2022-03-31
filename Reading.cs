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
