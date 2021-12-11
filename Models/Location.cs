using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BerryMap.Models
{
    [System.Serializable]
    public class Location
    {
        [System.NonSerialized]
        private Image display;

        public string Name { get; set; }

        public Image Display
        {
            get => display;
            set => display = value;
        }

        public Location(string name) => Name = name;

        public void DisplayLocation()
        {
            if (Display != null)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                string location = System.Reflection.Assembly.GetEntryAssembly().Location.Replace("BerryMap.exe", "flag.png");
                bitmap.UriSource = new Uri(location, UriKind.Absolute);
                bitmap.EndInit();
                Display.Source = bitmap;
            }
        }
    }
}
