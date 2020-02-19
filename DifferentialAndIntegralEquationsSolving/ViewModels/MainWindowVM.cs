using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DifferentialAndIntegralEquationsSolving.ViewModels.Equations;
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
        public MainWindowVM()
        {
            EulerVM = EulerView.eulerVM;
            RungeKuttaVM = RungeKuttaView.RungeKuttaVM;
            ThomasAlgoVM = ThomasAlgoView.ThomasAlgoVM;
            this.PlotViewModel = new PlotViewModel();
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
