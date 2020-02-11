using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;

namespace DifferentialAndIntegralEquationsSolving.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        public PlotViewModel plotViewModel;
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
