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
using System.Text.Json;
using System.Text.Json.Serialization;

public partial class GlowMqttMessage 
{
    [JsonPropertyName("ts")]
    public string? Timestamp { get; set; }
    [JsonPropertyName("hversion")]
    public string? HardwareVersion { get; set; }
    [JsonPropertyName("time")]
    public string? Time { get; set; }
    [JsonPropertyName("zbSoftVer")]
    public string? ZigbeeSoftVer { get; set; }
    [JsonPropertyName("smetsVer")]
    public string? SmetsVersion { get; set; }
    [JsonPropertyName("ets")]
    public string? Ets { get; set; }
    [JsonPropertyName("gid")]
    public string? Gid { get; set; }
    [JsonPropertyName("gmTime")]
    public string? Gmtime { get; set; }
    [JsonPropertyName("pan")]
    public Pan? Pan { get; set; }
    [JsonPropertyName("elecMtr")]
    public MeterData? Electricity { get; set; }
    [JsonPropertyName("gasMtr")]
    public MeterData? Gas { get; set; }
    [NotMapped]
    public DateTime TimestampUtc => Timestamp == null ? DateTime.UtcNow : DateTime.Parse(Timestamp);

    public static GlowMqttMessage? FromJson(string? json)
    {
        if(json != null)
        {
            try
            {
                var message = JsonSerializer.Deserialize<GlowMqttMessage>(json);

                return message;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error parsing JSON: {ex.Message}");
            }
        }

        return null;
    }
}
