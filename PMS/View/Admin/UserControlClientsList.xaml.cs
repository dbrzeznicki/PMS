﻿using System;
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
    /// Interaction logic for UserControlClientsList.xaml
    /// </summary>
    public partial class UserControlClientsList : UserControl
    {
        public UserControlClientsList()
        {
            InitializeComponent();
            ClientsListViewModel vm = new ClientsListViewModel();
            this.DataContext = vm;
        }
    }
}
