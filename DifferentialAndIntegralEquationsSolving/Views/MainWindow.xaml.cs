using DifferentialAndIntegralEquationsSolving.Equations;
using DifferentialAndIntegralEquationsSolving.ViewModels;
using DifferentialAndIntegralEquationsSolving.Views;
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
        public MainWindowVM MainWindowVM { get; set; }
        public MainWindow()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            InitializeComponent();
            MainWindowVM = new MainWindowVM();
            DataContext = MainWindowVM;
            //Euler euler = new Euler(0, 1, 0.01, 0);
            //euler.Solve();

            //RungeKutta rungeKutta = new RungeKutta(0.3, 1, 0.01, -3);
            //rungeKutta.Solve();


            //ThomasAlgo thomasAlgo = new ThomasAlgo(1, 2, 0.01, 1, 0);
            //thomasAlgo.Solve();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            if ((tabControl.SelectedContent as Grid).Children.OfType<EulerView>().FirstOrDefault() != null)
            {
                MainWindowVM.EulerVM.Euler.Solve();
                MainWindowVM.PlotViewModel.PlotXY(MainWindowVM.EulerVM.Euler.X, MainWindowVM.EulerVM.Euler.Y);
            }
            else if ((tabControl.SelectedContent as Grid).Children.OfType<RungeKuttaView>().FirstOrDefault() != null)
            {
                MainWindowVM.RungeKuttaVM.RungeKutta.Solve();
                MainWindowVM.PlotViewModel.PlotXY(MainWindowVM.RungeKuttaVM.RungeKutta.X, MainWindowVM.RungeKuttaVM.RungeKutta.Y);
            }
            else if ((tabControl.SelectedContent as Grid).Children.OfType<ThomasAlgoView>().FirstOrDefault() != null)
            {
                MainWindowVM.ThomasAlgoVM.ThomasAlgo.Solve();
                MainWindowVM.PlotViewModel.PlotXY(MainWindowVM.ThomasAlgoVM.ThomasAlgo.X, MainWindowVM.ThomasAlgoVM.ThomasAlgo.Y);
            }
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            if ((tabControl.SelectedContent as Grid).Children.OfType<EulerView>().FirstOrDefault() != null)
            {
                MainWindowVM.EulerVM.Euler.Export();
            }
            else if ((tabControl.SelectedContent as Grid).Children.OfType<RungeKuttaView>().FirstOrDefault() != null)
            {
                MainWindowVM.RungeKuttaVM.RungeKutta.Export();
            }
            else if ((tabControl.SelectedContent as Grid).Children.OfType<ThomasAlgoView>().FirstOrDefault() != null)
            {
                MainWindowVM.ThomasAlgoVM.ThomasAlgo.Export();
            }
        }
    }
}
