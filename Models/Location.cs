using System.Windows.Controls;
using System.Windows.Media;

namespace BerryMap.Models
{
    [System.Serializable]
    public class Location
    {
        [System.NonSerialized]
        private Label display;

        public string Name { get; set; }

        public Label Display
        {
            get => display;
            set => display = value;
        }

        public Location(string name, Label display)
        {
            Name = name;
            Display = display;
        }

        public Location (string name)
        {
            Name = name;
            Display = null;
        }

        public void DisplayLocation(Color color)
        {
            if (Display != null)
                Display.Background = new SolidColorBrush(color);
        }
    }
}
