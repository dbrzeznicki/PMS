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
    /// Interaction logic for UserControlProjectManager.xaml
    /// </summary>
    public partial class UserControlProjectManager : UserControl
    {
        public UserControlProjectManager()
        {
            InitializeComponent();
            ProjectManagerViewModel vm = new ProjectManagerViewModel();
            this.DataContext = vm;
        }
    }
}
