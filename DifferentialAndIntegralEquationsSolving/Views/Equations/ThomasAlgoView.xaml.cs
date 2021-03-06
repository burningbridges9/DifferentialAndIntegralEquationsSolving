﻿using DifferentialAndIntegralEquationsSolving.ViewModels.Equations;
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
    public partial class ThomasAlgoView : UserControl
    {
        public static ThomasAlgoVM ThomasAlgoVM;
        public ThomasAlgoView()
        {
            ThomasAlgoVM = new ThomasAlgoVM();
            InitializeComponent();
            DataContext = ThomasAlgoVM;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var x0 = double.Parse(X0.Text);
            var x1 = double.Parse(X1.Text);
            var h =  double.Parse(H.Text);
            var y0 = double.Parse(Y0.Text);
            var y1 = double.Parse(Y1.Text);

            ThomasAlgoVM.ThomasAlgo = new Equations.ThomasAlgo(x0, x1, h, y0, y1);
        }
    }
}
