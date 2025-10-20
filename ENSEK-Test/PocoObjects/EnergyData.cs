using System.Text.Json.Serialization;

namespace ENSEK_Test.PocoObjects
{
    public  class EnergyData
    {
        [JsonPropertyName("energy_id")]
        public int EnergyId { get; set; }

        [JsonPropertyName("price_per_unit")]
        public decimal PricePerUnit { get; set; }

        [JsonPropertyName("quantity_of_units")]
        public int QuantityOfUnits { get; set; }

        [JsonPropertyName("unit_type")]
        public string UnitType { get; set; }
    }
}
