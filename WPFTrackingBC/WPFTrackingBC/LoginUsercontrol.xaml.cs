using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices.AccountManagement;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFTrackingBC.Models;

namespace WPFTrackingBC
{
    /// <summary>
    /// Interaction logic for LoginUsercontrol.xaml
    /// </summary>
    public partial class LoginUsercontrol : UserControl
    {
        public LoginUsercontrol()
        {
            InitializeComponent();
            this.Loaded += LoginUsercontrol_Loaded;
        }

        private void LoginUsercontrol_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> Suppliers = new List<string>() { "OTR", "MRF", "BRIDGESTONE", "SOLID TYRES" };
            cmbSupplier.ItemsSource = Suppliers;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cmbUserType.SelectedIndex < 0)
            {
                MessageBox.Show("Please select user type");
            }
            else if(cmbUserType.SelectedIndex==5)
            {
                App.UserName = cmbSupplier.SelectedItem.ToString();
                App.mainWindow.AddChild(new SupplierUserControl());
            }
            else
            {
                App.UserType = (UserType)cmbUserType.SelectedIndex;
                App.UserName = txtUsername.Text;
                bool valid = false;
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
                {
                    valid = ValidateCredentials(txtUsername.Text, txtPassword.Password);
                }
                if (valid)
                {
                    App.mainWindow.AddChild(new ContainerListUserControl());
                }
                else
                {
                    MessageBox.Show("Invalid username and/or password");
                }
            }
        }

        private  bool ValidateCredentials(string text, string password)
        {
            try
            {
                SqlConnection conn = new SqlConnection(@"Server=idtp376;User=sa;Password=synerzip;Database=BlockChainDB;");
                conn.Open();

                string sql = String.Format("select * from dbo.Users where Name='{0}' and Password='{1}' and RoleId={2}", text, password, (int)App.UserType);

                SqlCommand cmd = new SqlCommand(sql);
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        return true;
                    }
                }
                return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
