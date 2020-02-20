using DifferentialAndIntegralEquationsSolving.MatlabSolvers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DifferentialAndIntegralEquationsSolving.ViewModels.MatlabSolvers
{
    public class RungeKuttaSolverVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public RungeKuttaSolverVM() : base()
        {
            this.RungeKuttaSolver = new RungeKuttaSolver();
        }

        private RungeKuttaSolver rungeKuttaSolver;
        public RungeKuttaSolver RungeKuttaSolver
        {
            get { return rungeKuttaSolver; }
            set
            {
                rungeKuttaSolver = value;
                OnPropertyChanged("RungeKuttaSolver");
            }
        }
    }
}
