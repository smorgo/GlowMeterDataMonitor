using System.Text.Json.Serialization;
// 0705: Prepayment
// - 00: Prepayment Information Set
//   - 00: PaymentControlConfiguration (bit map)
//   - 01: CreditRemaining (signed)
public class Prepayment
{
    [JsonPropertyName("00")]
    public PrepaymentInformationSet? PrepaymentInformationSet { get; set; }
}
