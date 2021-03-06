﻿using DifferentialAndIntegralEquationsSolving.Models.Equations;
using DifferentialAndIntegralEquationsSolving.ViewModels.Equations;
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

namespace DifferentialAndIntegralEquationsSolving.Views
{
    /// <summary>
    /// Interaction logic for EulerView.xaml
    /// </summary>
    public partial class DegenerateKernelView : UserControl
    {
        public static DegenerateKernelVM DegenerateKernelVM;
        public DegenerateKernelView()
        {
            DegenerateKernelVM = new DegenerateKernelVM();
            InitializeComponent();
            DataContext = DegenerateKernelVM;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            DegenerateKernelVM.DegenerateKernel = new DegenerateKernel();
        }
    }
}
