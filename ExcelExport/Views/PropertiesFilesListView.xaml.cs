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
using ExcelExport.ViewModels;

namespace ExcelExport.Views
{
    /// <summary>
    /// Interaction logic for PropertiesFilesListView.xaml
    /// </summary>
    public partial class PropertiesFilesListView : UserControl
    {
        public PropertiesFilesListView(PropertiesFileListViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
