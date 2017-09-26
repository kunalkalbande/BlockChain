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

namespace WPFTrackingBC
{
    /// <summary>
    /// Interaction logic for ContainerDetails.xaml
    /// </summary>
    public partial class ContainerDetails : Window
    {
        public ContainerDetails()
        {
            InitializeComponent();
            this.Loaded += ContainerDetails_Loaded;
        }

        private void ContainerDetails_Loaded(object sender, RoutedEventArgs e)
        {
            //List<string> Suppliers = new List<string>() { "OTR", "MRF", "BRIDGESTONE", "SOLID TYRES" };
            //cmbSupplier.ItemsSource = Suppliers;
            txtContainerId.Text = Guid.NewGuid().ToString().Replace("-", string.Empty);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
