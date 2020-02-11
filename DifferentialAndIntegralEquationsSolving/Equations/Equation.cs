using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DifferentialAndIntegralEquationsSolving.Equations
{
    public abstract class Equation : INotifyPropertyChanged
    {
        private int x0;
        public int X0
        {
            get { return x0; }
            set
            {
                X0 = value;
                OnPropertyChanged("X0");
            }
        }
        private int x1;
        public int X1
        {
            get { return x1; }
            set
            {
                X1 = value;
                OnPropertyChanged("X1");
            }
        }
        private int n;
        public int N
        {
            get { return n; }
            set
            {
                N = value;
                OnPropertyChanged("N");
            }
        }
        private double h;
        public double H
        {
            get { return h; }
            set
            {
                H = value;
                OnPropertyChanged("H");
            }
        }
        private double y0;
        public double Y0
        {
            get { return y0; }
            set
            {
                Y0 = value;
                OnPropertyChanged("Y0");
            }
        }
        public double []X { get; set; }
        public double []Y { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public abstract void Solve();
        protected void SetupX()
        {
            this.N = (int)((X1 - X0)/H);
            X = new double[N];
            for (int i =0; i<N;i++)
            {
                X[i] = X0 + i * H;
            }
        }
    }
}
