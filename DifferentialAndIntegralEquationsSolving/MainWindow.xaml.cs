using DifferentialAndIntegralEquationsSolving.Equations;
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

namespace DifferentialAndIntegralEquationsSolving
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Euler euler = new Euler(0, 1, 0.01, 0);
            //euler.Solve();

            //RungeKutta rungeKutta = new RungeKutta(0.3, 1, 0.01, -3);
            //rungeKutta.Solve();


            ThomasAlgo thomasAlgo = new ThomasAlgo(1, 2, 0.01, 1, 0);
            thomasAlgo.Solve();
        }
    }
}
