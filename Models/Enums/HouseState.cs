using System.ComponentModel;

namespace estatedocflow.api.Models.Enums
{
    public enum HouseState
    {
        [Description("Draft")]
        Draft,

        [Description("For Sale")]
        ForSale,

        [Description("Under Negotiation")]
        UnderNegotiation,

        [Description("Sold")]
        Sold
    }
}
