using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DifferentialAndIntegralEquationsSolving.Equations
{
    public abstract class Equation : INotifyPropertyChanged
    {
        public Equation()
        {

        }
        private double x0;
        public double X0
        {
            get { return x0; }
            set
            {
                x0 = value;
                OnPropertyChanged("X0");
            }
        }
        private double x1;
        public double X1
        {
            get { return x1; }
            set
            {
                x1 = value;
                OnPropertyChanged("X1");
            }
        }
        private int n;
        public int N
        {
            get { return n; }
            set
            {
                n = value;
                OnPropertyChanged("N");
            }
        }
        private double h;
        public double H
        {
            get { return h; }
            set
            {
                h = value;
                OnPropertyChanged("H");
            }
        }
        private double y0;
        public double Y0
        {
            get { return y0; }
            set
            {
                y0 = value;
                OnPropertyChanged("Y0");
            }
        }
        public double[] X { get; set; }
        public double[] Y { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public abstract void Solve();
        protected virtual void SetupX()
        {
            this.N = (int)((X1 - X0) / H);
            X = new double[N];
            for (int i = 0; i < N; i++)
            {
                X[i] = X0 + i * H;
            }
        }

        public void Export()
        {
            string pathX = Path.Combine(Directory.GetCurrentDirectory(), string.Format("{0}ExportedX.txt", this.GetType().Name)),
                pathY = Path.Combine(Directory.GetCurrentDirectory(), string.Format("{0}ExportedY.txt", this.GetType().Name));

            using (StreamWriter writerX = new StreamWriter(pathX, false, System.Text.Encoding.Default))
            using (StreamWriter writerY = new StreamWriter(pathY, false, System.Text.Encoding.Default))
            {
                foreach (var x in X)
                {
                    writerX.Write(x + " ");
                }
                foreach (var y in Y)
                {
                    writerY.Write(y + " ");
                }
            }
        }
    }
}
