using BerryMap.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BerryMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Plot> plots = new List<Plot>();

        private readonly string filePath = System.Reflection.Assembly.GetEntryAssembly().Location.Replace("BerryMap.exe", "berry.plant");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title = MessageOfTheDay.Message;
            // messageOfTheDayBlock.Text = MessageOfTheDay.Message;

            SetMap();

            TurnOffDisplays();

            // Load previous information.
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream fileStream = new FileStream(filePath, FileMode.Open);
                plots = (List<Plot>)formatter.Deserialize(fileStream);
                fileStream.Close();

                FillPlotLocationLabels();
            }
            catch (FileNotFoundException)
            {
                InstantiateNewPlots();
            }

            PopulatePlots();
        }

        private void SetMap()
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            string location = System.Reflection.Assembly.GetEntryAssembly().Location.Replace("BerryMap.exe", "sinnohmap.png");
            bitmap.UriSource = new Uri(location, UriKind.Absolute);
            bitmap.EndInit();
            Map.Source = bitmap;
        }

        private void Routes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TurnOffDisplays();

            if (Routes.SelectedItem != null)
                (Routes.SelectedItem as Plot).Location.DisplayLocation(Colors.Green);
        }

        private void TurnOffDisplays()
        {
            PastoriaCity.Background = new SolidColorBrush(Colors.Transparent);
            FloaromaTown.Background = new SolidColorBrush(Colors.Transparent);
            Route208.Background = new SolidColorBrush(Colors.Transparent);
            Route221.Background = new SolidColorBrush(Colors.Transparent);
            FuegoIronworks.Background = new SolidColorBrush(Colors.Transparent);
            SolaceonTown.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void Routes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Routes.SelectedItem != null)
            {
                Plot selection = Routes.SelectedItem as Plot;
                PlantBerry plantBerry = new PlantBerry(selection);

                plantBerry.Icon = BitmapFrame.Create(new Uri(APIController.GetBerryIcon()));

                plantBerry.ShowDialog();

                if (plantBerry.DialogResult == true)
                {
                    // Update the plot.
                    plots[Routes.SelectedIndex] = selection;
                }

                // Refresh the list.
                PopulatePlots();
            }
        }

        private void PopulatePlots()
        {
            Routes.Items.Clear();
            
            foreach (Plot plot in plots)
            {
                Routes.Items.Add(plot);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // Save the information.
            BinaryFormatter formatter = new BinaryFormatter();

            using (Stream stream = File.Create(filePath))
            {
                formatter.Serialize(stream, plots);
            }
        }

        private void FillPlotLocationLabels()
        {
            plots.Find(x => x.Location.Name.Contains("Floaroma")).Location.Display = FloaromaTown;
            plots.Find(x => x.Location.Name.Contains("Pastoria")).Location.Display = PastoriaCity;
            plots.Find(x => x.Location.Name.Contains("208")).Location.Display = Route208;
            plots.Find(x => x.Location.Name.Contains("Ironworks")).Location.Display = FuegoIronworks;
            plots.Find(x => x.Location.Name.Contains("221")).Location.Display = Route221;
            plots.Find(x => x.Location.Name.Contains("Solaceon")).Location.Display = SolaceonTown;
        }

        private void InstantiateNewPlots()
        {
            plots.Add(new Plot(4, new Location("Route 208", Route208)));
            plots.Add(new Plot(2, new Location("Floaroma Town", FloaromaTown)));
            plots.Add(new Plot(2, new Location("Route 212")));
            plots.Add(new Plot(2, new Location("Route 221", Route221)));
            plots.Add(new Plot(2, new Location("Route 209 (1)")));
            plots.Add(new Plot(2, new Location("Route 209 (2)")));
            plots.Add(new Plot(4, new Location("Solaceon Town", SolaceonTown)));
            plots.Add(new Plot(4, new Location("Route 210")));
            plots.Add(new Plot(2, new Location("Route 215 (1)")));
            plots.Add(new Plot(2, new Location("Route 215 (2)")));
            plots.Add(new Plot(4, new Location("Route 214")));
            plots.Add(new Plot(4, new Location("Pastoria City", PastoriaCity)));
            plots.Add(new Plot(4, new Location("Route 212")));
            plots.Add(new Plot(4, new Location("Route 213")));
            plots.Add(new Plot(4, new Location("Route 210")));
            plots.Add(new Plot(4, new Location("Route 211")));
            plots.Add(new Plot(4, new Location("Route 207")));
            plots.Add(new Plot(2, new Location("Route 206 (1)")));
            plots.Add(new Plot(2, new Location("Route 206 (2)")));
            plots.Add(new Plot(4, new Location("Fuego Ironworks", FuegoIronworks)));
            plots.Add(new Plot(4, new Location("Route 205 (1)")));
            plots.Add(new Plot(2, new Location("Route 205 (2)")));
            plots.Add(new Plot(2, new Location("Route 205 (3)")));
            plots.Add(new Plot(4, new Location("Eterna City")));
            plots.Add(new Plot(4, new Location("Route 218")));
            plots.Add(new Plot(4, new Location("Route 220")));
        }
    }
}
