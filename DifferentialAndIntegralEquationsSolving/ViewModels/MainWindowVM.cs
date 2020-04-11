using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DifferentialAndIntegralEquationsSolving.ViewModels.Equations;
using DifferentialAndIntegralEquationsSolving.ViewModels.MatlabSolvers;
using DifferentialAndIntegralEquationsSolving.Views;
using OxyPlot;

namespace DifferentialAndIntegralEquationsSolving.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        public PlotViewModel plotViewModel;
        public EulerVM EulerVM { get; set; }
        public RungeKuttaVM RungeKuttaVM { get; set; }
        public ThomasAlgoVM ThomasAlgoVM { get; set; }
        public CollocationVM CollocationVM { get; set; }
        public GalerkinVM GalerkinVM { get; set; }
        public EulerSolverVM EulerSolverVM { get; set; }
        public RungeKuttaSolverVM RungeKuttaSolverVM { get; set; }
        public TikhonovRegularizationGalerkinVM TikhonovRegularizationGalerkinVM { get; set; }
        public MainWindowVM()
        {
            EulerVM = EulerView.eulerVM;
            RungeKuttaVM = RungeKuttaView.RungeKuttaVM;
            ThomasAlgoVM = ThomasAlgoView.ThomasAlgoVM;
            CollocationVM = CollocationView.CollocationVM;
            GalerkinVM = GalerkinView.GalerkinVM;
            TikhonovRegularizationGalerkinVM = TikhonovRegularizationGalerkinView.TikhonovRegularizationGalerkinVM;
            //RungeKuttaSolverVM = new RungeKuttaSolverVM();
            // EulerSolverVM = new EulerSolverVM();
            PlotViewModel = new PlotViewModel();
        }
        public PlotViewModel PlotViewModel
        {
            get { return plotViewModel; }
            set
            {
                plotViewModel = value;
                OnPropertyChanged("PlotViewModel");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
