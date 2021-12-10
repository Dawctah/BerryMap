using Newtonsoft.Json;

namespace BerryMap.Models
{
    [System.Serializable]
    public class Berry
    {
        public string Name { get; set; }

        public string URL { get; set; }

        [JsonProperty("growth_time")]
        public int GrowthTime { get; set; }

        [JsonProperty("max_harvest")]
        public int MaxHarvest { get; set; }

        [JsonProperty("soil_dryness")]
        public int SoilDryness { get; set; }

        public override string ToString() => Capitalize(Name);

        private string Capitalize(string input) => input.Substring(0, 1).ToUpper() + input.Substring(1).ToLower();
    }
}
