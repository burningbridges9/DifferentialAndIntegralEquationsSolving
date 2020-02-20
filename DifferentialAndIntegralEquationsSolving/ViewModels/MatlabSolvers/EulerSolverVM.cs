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
    public class EulerSolverVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public EulerSolverVM() : base()
        {
            this.EulerSolver = new EulerSolver();
        }

        private EulerSolver eulerSolver;
        public EulerSolver EulerSolver
        {
            get { return eulerSolver; }
            set
            {
                eulerSolver = value;
                OnPropertyChanged("EulerSolver");
            }
        }
    }
}
