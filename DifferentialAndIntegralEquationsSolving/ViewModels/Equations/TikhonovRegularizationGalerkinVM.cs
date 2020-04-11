using DifferentialAndIntegralEquationsSolving.Equations;
using DifferentialAndIntegralEquationsSolving.Models.Equations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DifferentialAndIntegralEquationsSolving.ViewModels.Equations
{
    public class TikhonovRegularizationGalerkinVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public TikhonovRegularizationGalerkinVM()
        {
            //this.Euler = new Euler();
        }

        private TikhonovRegularizationGalerkin tikhonovRegularizationGalerkin;
        public TikhonovRegularizationGalerkin TikhonovRegularizationGalerkin
        {
            get { return tikhonovRegularizationGalerkin; }
            set
            {
                tikhonovRegularizationGalerkin = value;
                OnPropertyChanged("TikhonovRegularizationGalerkin");
            }
        }
    }
}
