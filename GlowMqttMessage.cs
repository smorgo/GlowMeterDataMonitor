/*
{
    "elecMtr": {
        "0702":{
            "03":{
                "01":"00000001",
                "04":"00",
                "02":"000003E8",
                "07":"1416293100004",
                "03":"29",
                "08":"19K0181107",
                "00":"00",
                "06":"00"
            },
            "00":{
                "07":"00000000",
                "01":"000000000020",
                "00":"0000019A0B0D",
                "14":"02",
                "02":"000000000000"
            },
            "04":{
                "01":"0044ED",
                "40":"11BF5D",
                "30":"02488B",
                "00":"00000643"
            },
            "02":{
                "00":"01"
            }
        },
        "0705":{
            "00":{
                "01":"0024BB14",
                "00":"01D4"
            }
        },
        "0708":{
            "01":{
                "01":"Octopus Energy"
            }
        }
    },
    "gasMtr":{
        "0702":{
            "03":{
                "01":"00000001",
                "12":"00",
                "02":"000003E8",
                "07":"2485989906",
                "03":"AB",
                "08":"",
                "00":"01",
                "06":"80"
            },
            "00":{
                "00":"00000031FF53",
                "14":"02"
            },
            "0C":{
                "01":"0098A6",
                "40":"13DF68",
                "30":"02527D"
            },
            "02":{
                "00":"00"
            }
        },
        "0705":{
            "00":{
                "01":"00000000",
                "00":"0C94"
            }
        },
        "0708":{
            "01":{
                "01":""
            }
        }
    },
    "ts":"2022-03-31 14:56:45",
    "hversion":"GLOW-IHD-01-1v4-SMETS2",
    "time":"6245C12D",
    "zbSoftVer":"1.2.5",
    "gmtime":1648738605,
    "pan":{
        "rssi":"B5",
        "status":"joined",
        "nPAN":"00",
        "join":"0",
        "lqi":"64"
    },
    "smetsVer":"SMETS2",
    "ets":"2000-01-01 00:00:00",
    "gid":"70B3D521E000E6BB"
}
*/
/*
// Fields gathered from the ZigBee Smart Energy Standard document
// 0702: Metering
// - 00: Reading Information Set
//   - 00: CurrentSummationDelivered: meter reading
//   - 01: CurrentSummationReceived
//   - 02: CurrentMaxDemandDelivered
//   - 07: ReadingSnapshotTime (UTC time)
//   - 14: Supply Status (enum): 0x2 is on
// - 02: Meter Status
//   - 00: Status (bit map): 10 means power quality event
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
// - 04: Historical Consumption
//   - 00: InstantaneousDemand (signed): current consumption
//   - 01: CurrentDayConsumptionDelivered
//   - 30: CurrentWeekConsumptionDelivered
//   - 40: CurrentMonthConsumptionDelivered
// - 0C: Alternative Historical Consumption
//   - 01: CurrentDayConsumptionDelivered
//   - 30: CurrentWeekConsumptionDelivered
//   - 40: CurrentMonthConsumptionDelivered
// 0705: Prepayment
// - 00: Prepayment Information Set
//   - 00: PaymentControlConfiguration (bit map)
//   - 01: CreditRemaining (signed)
// 0708: Device Management
// - 01: Supplier Control Attribute Set
//   - 01: ProviderName (string)
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
