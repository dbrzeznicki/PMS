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

namespace PMS.Algorithm
{
    /// <summary>
    /// Interaction logic for CocomoDetailView.xaml
    /// </summary>
    public partial class CocomoDetailView : Window
    {
        public CocomoDetailView()
        {
            InitializeComponent();
            CocomoDetail vm = new CocomoDetail();
            this.DataContext = vm;
        }
    }
}
