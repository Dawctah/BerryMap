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
using Utilities;

namespace BerryMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Plot> plots = new List<Plot>();

        private string exe = "BerryMap.exe";

        private string FilePath => System.Reflection.Assembly.GetEntryAssembly().Location.Replace(exe, "berry.plant");

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(string exe)
        {
            this.exe = exe;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            messageOfTheDayBlock.Text = MessageOfTheDay.Message;

            ShowPlotsButton.Visibility = Visibility.Hidden;
            Berries.Visibility = Visibility.Hidden;

            SetMap(exe);

            TurnOffDisplays();

            // Load previous data.
            plots = Data.LoadData<List<Plot>, FileNotFoundException>(plots, FilePath, InstantiateNewPlots) as List<Plot>;

            FillPlotLocationImages();
            PopulatePlots();
        }

        private void SetMap(string exe)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            string location = System.Reflection.Assembly.GetEntryAssembly().Location.Replace(exe, "sinnohmap.png");
            bitmap.UriSource = new Uri(location, UriKind.Absolute);
            bitmap.EndInit();
            Map.Source = bitmap;
        }

        private void Routes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TurnOffDisplays();

            if (Routes.SelectedItem != null)
                (Routes.SelectedItem as Plot).Location.DisplayLocation();
        }

        private void TurnOffDisplays()
        {
            Hide(Route208);
            Hide(FloaromaTown);
            Hide(Route212_1);
            Hide(Route212_2);
            Hide(Route212_3);
            Hide(Route221);
            Hide(Route209_1);
            Hide(Route209_2);
            Hide(SolaceonTown);
            Hide(Route210_1);
            Hide(Route210_2);
            Hide(Route215_1);
            Hide(Route215_2);
            Hide(Route214);
            Hide(PastoriaCity);
            Hide(Route213);
            Hide(Route211);
            Hide(Route207);
            Hide(Route206_1);
            Hide(Route206_2);
            Hide(Ironworks);
            Hide(Route205_1);
            Hide(Route205_2);
            Hide(Route205_3);
            Hide(EternaForest);
            Hide(Route218);
        }

        private void Hide(Image image, bool hide = true)
        {
            /*BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            string location = System.Reflection.Assembly.GetEntryAssembly().Location.Replace("BerryMap.exe", "BerryIcon.png");
            bitmap.UriSource = new Uri(location, UriKind.Absolute);
            bitmap.EndInit();
            image.Source = bitmap;*/
            
            if (hide)
                image.Visibility = Visibility.Hidden;
            else
                image.Visibility = Visibility.Visible;
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
            plots.Remove(plots.Find(x => x.Location.Name == "Route 220"));
            
            foreach (Plot plot in plots)
            {
                if (plot.Location.Name == "Eterna City")
                    plot.Location.Name = "Eterna Forest";

                Routes.Items.Add(plot);
            }

            Berries.Items.Clear();
            List<BerryViewModel> berries = new List<BerryViewModel>();
            int emptySlots = 0;
            foreach(Plot plot in plots)
            {
                foreach (Berry berry in plot.Berries)
                {
                    if (berry != null)
                    {
                        if (berries.Find(x => x.Berry.Name == berry.Name) == null)
                            berries.Add(new BerryViewModel() { Berry = berry, Quantity = 1 });
                        else
                            berries.Find(x => x.Berry.Name == berry.Name).Quantity++;
                    }
                    else
                        emptySlots++;
                }
            }

            if (emptySlots != 0)
                berries.Add(new BerryViewModel() { Berry = new Berry() { Name = "Empty" }, Quantity = emptySlots });

            foreach (BerryViewModel berry in berries)
            {
                Berries.Items.Add(berry);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // Save the information.
            BinaryFormatter formatter = new BinaryFormatter();

            using (Stream stream = File.Create(FilePath))
            {
                formatter.Serialize(stream, plots);
            }
        }

        private void FillPlotLocationImages()
        {
            FillPlotImage(0, Route208);
            FillPlotImage(1, FloaromaTown);
            FillPlotImage(2, Route212_1);
            FillPlotImage(3, Route212_2);
            FillPlotImage(4, Route212_3);
            FillPlotImage(5, Route221);
            FillPlotImage(6, Route209_1);
            FillPlotImage(7, Route209_2);
            FillPlotImage(8, SolaceonTown);
            FillPlotImage(9, Route210_1);
            FillPlotImage(10, Route210_2);
            FillPlotImage(11, Route215_1);
            FillPlotImage(12, Route215_2);
            FillPlotImage(13, Route214);
            FillPlotImage(14, PastoriaCity);
            FillPlotImage(15, Route213);
            FillPlotImage(16, Route211);
            FillPlotImage(17, Route207);
            FillPlotImage(18, Route206_1);
            FillPlotImage(19, Route206_2);
            FillPlotImage(20, Ironworks);
            FillPlotImage(21, Route205_1);
            FillPlotImage(21, Route205_2);
            FillPlotImage(22, Route205_3);
            FillPlotImage(23, EternaForest);
            FillPlotImage(24, Route218);
        }

        private void FillPlotImage(int index, Image image)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            string location = System.Reflection.Assembly.GetEntryAssembly().Location.Replace(exe, "BerryIcon.png");
            bitmap.UriSource = new Uri(location, UriKind.Absolute);
            bitmap.EndInit();
            image.Source = bitmap;

            plots[index].Location.Display = image;
        }

        private void InstantiateNewPlots()
        {
            plots.Add(new Plot(4, new Location("Route 208")));
            plots.Add(new Plot(2, new Location("Floaroma Town")));
            plots.Add(new Plot(2, new Location("Route 212 (1)")));
            plots.Add(new Plot(2, new Location("Route 212 (2)")));
            plots.Add(new Plot(4, new Location("Route 212 (3)")));
            plots.Add(new Plot(2, new Location("Route 221")));
            plots.Add(new Plot(2, new Location("Route 209 (1)")));
            plots.Add(new Plot(2, new Location("Route 209 (2)")));
            plots.Add(new Plot(4, new Location("Solaceon Town")));
            plots.Add(new Plot(4, new Location("Route 210 (1)")));
            plots.Add(new Plot(4, new Location("Route 210 (2)")));
            plots.Add(new Plot(2, new Location("Route 215 (1)")));
            plots.Add(new Plot(2, new Location("Route 215 (2)")));
            plots.Add(new Plot(4, new Location("Route 214")));
            plots.Add(new Plot(4, new Location("Pastoria City")));
            plots.Add(new Plot(4, new Location("Route 213")));
            plots.Add(new Plot(4, new Location("Route 211")));
            plots.Add(new Plot(4, new Location("Route 207")));
            plots.Add(new Plot(2, new Location("Route 206 (1)")));
            plots.Add(new Plot(2, new Location("Route 206 (2)")));
            plots.Add(new Plot(4, new Location("Fuego Ironworks")));
            plots.Add(new Plot(4, new Location("Route 205 (1)")));
            plots.Add(new Plot(2, new Location("Route 205 (2)")));
            plots.Add(new Plot(2, new Location("Route 205 (3)")));
            plots.Add(new Plot(4, new Location("Eterna Forest")));
            plots.Add(new Plot(4, new Location("Route 218")));
        }

        private void ShowBerryCount_Click(object sender, RoutedEventArgs e)
        {
            // Change the list to display berries.
            Berries.Visibility = Visibility.Visible;
            ShowPlotsButton.Visibility = Visibility.Visible;

            Routes.Visibility = Visibility.Hidden;
            ShowBerryCount.Visibility = Visibility.Hidden;
        }

        private void ShowPlotsButton_Click(object sender, RoutedEventArgs e)
        {
            // Change the list to display plots.
            Berries.Visibility = Visibility.Hidden;
            ShowPlotsButton.Visibility = Visibility.Hidden;

            Routes.Visibility = Visibility.Visible;
            ShowBerryCount.Visibility = Visibility.Visible;
        }
    }
}
