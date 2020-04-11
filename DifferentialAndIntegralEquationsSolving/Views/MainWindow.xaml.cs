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
            else if ((tabControl.SelectedContent as Grid).Children.OfType<CollocationView>().FirstOrDefault() != null)
            {
                MainWindowVM.CollocationVM.Collocation.Solve();
                MainWindowVM.PlotViewModel.PlotXY(MainWindowVM.CollocationVM.Collocation.X, MainWindowVM.CollocationVM.Collocation.Y);
            }
            else if ((tabControl.SelectedContent as Grid).Children.OfType<GalerkinView>().FirstOrDefault() != null)
            {
                MainWindowVM.GalerkinVM.Galerkin.Solve();
                MainWindowVM.PlotViewModel.PlotXY(MainWindowVM.GalerkinVM.Galerkin.X, MainWindowVM.GalerkinVM.Galerkin.Y);
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

        private void MatlabSolve_Click(object sender, RoutedEventArgs e)
        {
            if ((tabControl.SelectedContent as Grid).Children.OfType<EulerView>().FirstOrDefault() != null)
            {
                var x0 = MainWindowVM.EulerVM.Euler.X0;
                var x1 = MainWindowVM.EulerVM.Euler.X1;
                var h = MainWindowVM.EulerVM.Euler.H;
                var y0 = MainWindowVM.EulerVM.Euler.Y0;
                MainWindowVM.EulerSolverVM.EulerSolver.Execute("Euler", x0, x1, h, y0);

                MainWindowVM.PlotViewModel.PlotXYXY(MainWindowVM.EulerVM.Euler.X, MainWindowVM.EulerVM.Euler.Y,
                    MainWindowVM.EulerSolverVM.EulerSolver.X, MainWindowVM.EulerSolverVM.EulerSolver.Y);
            }
            else if ((tabControl.SelectedContent as Grid).Children.OfType<RungeKuttaView>().FirstOrDefault() != null)
            {
                var x0 = MainWindowVM.RungeKuttaVM.RungeKutta.X0;
                var x1 = MainWindowVM.RungeKuttaVM.RungeKutta.X1;
                var h = MainWindowVM.RungeKuttaVM.RungeKutta.H;
                var y0 = MainWindowVM.RungeKuttaVM.RungeKutta.Y0;
                MainWindowVM.RungeKuttaSolverVM.RungeKuttaSolver.Execute("RungeKutta", x0, x1, h, y0);

                MainWindowVM.PlotViewModel.PlotXYXY(MainWindowVM.RungeKuttaVM.RungeKutta.X, MainWindowVM.RungeKuttaVM.RungeKutta.Y,
                    MainWindowVM.RungeKuttaSolverVM.RungeKuttaSolver.X, MainWindowVM.RungeKuttaSolverVM.RungeKuttaSolver.Y);
            }
            //else if ((tabControl.SelectedContent as Grid).Children.OfType<ThomasAlgoView>().FirstOrDefault() != null)
            //{
            //    MainWindowVM.ThomasAlgoVM.ThomasAlgo.Export();
            //}
        }

        private void Diff_Click(object sender, RoutedEventArgs e)
        {
            var diffY = new double[MainWindowVM.RungeKuttaVM.RungeKutta.Y.Length];
            for (int i = 0; i < MainWindowVM.RungeKuttaVM.RungeKutta.Y.Length; i++)
            {
                diffY[i] = Math.Abs(MainWindowVM.RungeKuttaVM.RungeKutta.Y[i] - MainWindowVM.RungeKuttaSolverVM.RungeKuttaSolver.Y[i]);
            }
            MainWindowVM.PlotViewModel.PlotXY(MainWindowVM.RungeKuttaVM.RungeKutta.X, diffY);
        }
        

        private void DiffIntegr_Click(object sender, RoutedEventArgs e)
        {
            if ((tabControl.SelectedContent as Grid).Children.OfType<CollocationView>().FirstOrDefault() != null)
            {
                MainWindowVM.CollocationVM.Collocation.Tolerance();
                MainWindowVM.PlotViewModel.PlotXY(MainWindowVM.CollocationVM.Collocation.X, MainWindowVM.CollocationVM.Collocation.Diff);
            }
            else if ((tabControl.SelectedContent as Grid).Children.OfType<GalerkinView>().FirstOrDefault() != null)
            {
                MainWindowVM.GalerkinVM.Galerkin.Tolerance();
                MainWindowVM.PlotViewModel.PlotXY(MainWindowVM.GalerkinVM.Galerkin.X, MainWindowVM.GalerkinVM.Galerkin.Diff);
            }
        }
    }
}
