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
    public class EulerVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public EulerVM()
        {
            //this.Euler = new Euler();
        }

        private Euler euler;
        public Euler Euler
        {
            get { return euler; }
            set
            {
                euler = value;
                OnPropertyChanged("Euler");
            }
        }
    }
}
