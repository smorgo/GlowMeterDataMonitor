using System.Text.Json.Serialization;
// - 00: Prepayment Information Set
//   - 00: PaymentControlConfiguration (bit map)
//   - 01: CreditRemaining (signed)
public class PrepaymentInformationSet
{
    [JsonPropertyName("00")]
    public string? PaymentControlConfiguration { get; set; }
    [JsonPropertyName("01")]
    public string? CreditRemaining { get; set; }
}
