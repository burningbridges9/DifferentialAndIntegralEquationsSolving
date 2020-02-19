using DifferentialAndIntegralEquationsSolving.Equations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DifferentialAndIntegralEquationsSolving.ViewModels.Equations
{
    public class RungeKuttaVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public RungeKuttaVM()
        {
            //this.Euler = new Euler();
        }

        private RungeKutta rungeKutta;
        public RungeKutta RungeKutta
        {
            get { return rungeKutta; }
            set
            {
                rungeKutta = value;
                OnPropertyChanged("RungeKutta");
            }
        }
    }
}
