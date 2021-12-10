using BerryMap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BerryMap
{
    /// <summary>
    /// Interaction logic for PlantBerry.xaml
    /// </summary>
    public partial class PlantBerry : Window
    {
        private Plot plot;
        private List<ComboBox> comboBoxes = new List<ComboBox>();

        public PlantBerry(Plot plot)
        {
            this.plot = plot;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            double leftMargin = 10;
            
            List<Berry> berries = new List<Berry>();
            berries.Add(new Berry() { Name = "Empty" });

            List<Berry> allBerries = APIController.GetBerries();

            foreach (Berry berry in allBerries)
                berries.Add(berry);

            for (int k = 0; k < plot.Berries.Length; k++)
            {
                Berry berry = plot.Berries[k];

                ComboBox comboBox = new ComboBox();
                comboBox.ItemsSource = berries;
                if (berry != null)
                {
                    comboBox.SelectedItem = berries.Find(x => x.Name == berry.Name);
                }
                else
                    comboBox.SelectedItem = berries[0];

                comboBox.Name = "BerrySlot" + k;
                comboBox.HorizontalAlignment = HorizontalAlignment.Left;
                comboBox.VerticalAlignment = VerticalAlignment.Top;
                comboBox.Margin = new Thickness(leftMargin, 10, 0, 0);
                leftMargin += 125;
                comboBox.Width = 120;
                comboBox.Height = 25;

                Wrapper.Children.Add(comboBox);
                comboBoxes.Add(comboBox);
            }
        }

        private void Plant_Click(object sender, RoutedEventArgs e)
        {
            for (int k = 0; k < comboBoxes.Count; k++)
            {
                plot.Berries[k] = comboBoxes[k].SelectedItem as Berry;
                if (plot.Berries[k].Name == "Empty")
                    plot.Berries[k] = null;
            }

            DialogResult = true;
        }
    }
}
