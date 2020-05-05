using PMS.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace PMS
{
    /// <summary>
    /// Interaction logic for UserControlWikiEmployee.xaml
    /// </summary>
    public partial class UserControlWikiEmployee : UserControl
    {
        public UserControlWikiEmployee()
        {
            InitializeComponent();
            WikiEmployeeViewModel vm = new WikiEmployeeViewModel();
            this.DataContext = vm;
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = (Article)(WikiList.SelectedItem ?? "(none)");
            OpenUrl(selectedItem.Url);
        }

        private void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else
                    throw;
            }
        }

    }
}
