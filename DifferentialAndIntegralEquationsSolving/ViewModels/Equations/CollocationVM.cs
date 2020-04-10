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
    public class CollocationVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public CollocationVM()
        {
            //this.Euler = new Euler();
        }

        private Collocation collocation;
        public Collocation Collocation
        {
            get { return collocation; }
            set
            {
                collocation = value;
                OnPropertyChanged("Collocation");
            }
        }
    }
}
