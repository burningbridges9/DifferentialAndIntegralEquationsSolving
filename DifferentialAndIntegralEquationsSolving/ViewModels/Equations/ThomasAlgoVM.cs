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
    public class ThomasAlgoVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public ThomasAlgoVM()
        {
            //this.Euler = new Euler();
        }

        private ThomasAlgo thomasAlgo;
        public ThomasAlgo ThomasAlgo
        {
            get { return thomasAlgo; }
            set
            {
                thomasAlgo = value;
                OnPropertyChanged("ThomasAlgo");
            }
        }
    }
}
