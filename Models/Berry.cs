using Newtonsoft.Json;

namespace BerryMap.Models
{
    [System.Serializable]
    public class Berry
    {
        [System.NonSerialized]
        private Item item;

        public string Name { get; set; }

        public string URL { get; set; }

        public string Image { get; set; }

        public override string ToString() => Capitalize(Name);

        private string Capitalize(string input) => input.Substring(0, 1).ToUpper() + input.Substring(1).ToLower();

        [JsonProperty("item")]
        public Item Item
        {
            get => item;
            set => item = value;
        }
    }

    public class BerryViewModel
    {
        public Berry Berry { get; set; }
        public int Quantity { get; set; }

        public override string ToString() => Berry.ToString() + "\t\t\t\t\t\tx" + Quantity;
    }

    public class Item
    {
        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("sprites")]
        public Sprite Sprite { get; set; }
    }

    public class Sprite
    {
        [JsonProperty("default")]
        public string Image { get; set; }
    }
}
