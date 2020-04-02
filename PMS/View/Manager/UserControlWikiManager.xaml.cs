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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PMS
{
    /// <summary>
    /// Interaction logic for UserControlWikiManager.xaml
    /// </summary>
    public partial class UserControlWikiManager : UserControl
    {
        public UserControlWikiManager()
        {
            InitializeComponent();
            WikiManagerViewModel vm = new WikiManagerViewModel();
            this.DataContext = vm;
        }
    }
}
