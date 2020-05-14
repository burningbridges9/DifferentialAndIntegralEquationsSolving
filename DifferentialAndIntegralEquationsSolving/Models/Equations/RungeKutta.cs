using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentialAndIntegralEquationsSolving.Equations
{
    public class RungeKutta : Equation
    {
        public RungeKutta(double x0, double x1, double h, double y0)
        {
            this.X0 = x0;
            this.X1 = x1;
            this.H = h;
            this.Y0 = y0;
            this.SetupX();
            this.Y = new double[N];
            Y[0] = Y0;
        }

        private double K(double y)
        {
            var val = 4 * Math.Pow(y, 2) + 2;
            return val;
        }
        public override void Solve()
        {
            for (int i = 1; i < X.Length; i++)
            {
                var k1 = K(Y[i - 1]);
                var k2 = K(Y[i - 1] + H * k1 / 2.0);
                var k3 = K(Y[i - 1] + H * k2 / 2.0);
                var k4 = K(Y[i - 1] + H * k3);
                Y[i] = Y[i - 1] + (H / 6.0) * (k1 + 2 * k2 + 2 * k3 + k4);
            }
        }

        public void Tolerance()
        {
            var Y_exact = new double[N];
            for (int i = 0; i < N; i++)
            {
                Y_exact[i] = YExact(X[i]);
            }
            Diff = new double[N];
            for (int j = 0; j < X.Length; j++)
            {
                Diff[j] = Math.Abs(Y[j] - Y_exact[j]);
            }
        }
        public double YExact(double xi)
        {
            var retVal = -(Math.Sqrt(2) * Math.Tan(Math.Atan(3 * Math.Sqrt(2)) - 2 * Math.Sqrt(2) * xi + (3 * Math.Sqrt(2)) / 5.0)) / 2.0;
            return retVal;
        }
    }
}
